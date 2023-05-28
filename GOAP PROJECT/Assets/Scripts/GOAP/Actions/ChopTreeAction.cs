using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopTreeAction : BaseAction
{
    protected Vector3 targetPosition;
    protected GameObject treeObject;
    private bool isChopping = false;
    private float choppingTimer = 5f;
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(GetLogsGoal) });

    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }

    public void SetTreeObject(GameObject tree)
    {
        treeObject = tree;
    }

    public override void OnActivated(BaseGoal linkedGoal)
    {
        base.OnActivated(linkedGoal);

        // Find the nearest tree
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        GameObject nearestTree = FindNearestTree(trees);

        // Move towards the nearest tree using A* pathfinding
        if (nearestTree != null)
        {
            // Set the target position for the ChopTreeAction
            SetTargetPosition(nearestTree.transform.position);

            // Set the tree object for the ChopTreeAction
            SetTreeObject(nearestTree);

            // Move to the target position
            goapAgent.MoveTo(targetPosition);
        }
    }

    public override void OnDeactivated()
    {
        // Clear the tree object from the map if the action is interrupted or completed
        if (treeObject != null)
        {
            TreeManager treeManager = FindObjectOfType<TreeManager>();
            treeManager.ClearTree(treeObject);
        }

        base.OnDeactivated();
    }

    public override void OnTick()
    {
        // Check if the AI is already at the tree
        if (Vector3.Distance(goapAgent.transform.position, targetPosition) <= goapAgent.reachDistance)
        {
            if (!isChopping)
            {
                // Start chopping the tree
                isChopping = true;
                choppingTimer = 5f; // Set the chopping timer to 5 seconds
            }
            else
            {
                // Continue chopping the tree
                choppingTimer -= Time.deltaTime;

                if (choppingTimer <= 0f)
                {
                    // AI has finished chopping the tree
                    characterStats.IncreaseWood(1);

                    // Clear the tree object from the map
                    if (treeObject != null)
                    {
                        TreeManager treeManager = FindObjectOfType<TreeManager>();
                        treeManager.ClearTree(treeObject);
                        treeObject = null;
                    }

                    // Move to the next tree if there is one available
                    GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
                    GameObject nearestTree = FindNearestTree(trees);
                    if (nearestTree != null)
                    {
                        // Set the target position for the ChopTreeAction
                        SetTargetPosition(nearestTree.transform.position);

                        // Set the tree object for the ChopTreeAction
                        SetTreeObject(nearestTree);

                        // Reset the chopping variables
                        isChopping = false;
                        choppingTimer = 5f;

                        // Move to the target position
                        goapAgent.MoveTo(targetPosition);
                    }
                    else
                    {
                        // Stop the agent by setting its target position to its current position
                        goapAgent.MoveTo(goapAgent.transform.position);
                    }
                }
            }
        }
    }

    private GameObject FindNearestTree(GameObject[] trees)
    {
        GameObject nearestTree = null;
        float nearestDistance = Mathf.Infinity;
        Vector3 agentPosition = goapAgent.transform.position;

        foreach (GameObject tree in trees)
        {
            float distance = Vector3.Distance(agentPosition, tree.transform.position);
            if (distance < nearestDistance)
            {
                nearestTree = tree;
                nearestDistance = distance;
            }
        }

        return nearestTree;
    }
}
