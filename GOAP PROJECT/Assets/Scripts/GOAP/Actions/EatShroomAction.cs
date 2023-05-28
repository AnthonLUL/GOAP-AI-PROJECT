using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatShroomAction : BaseAction
{
    protected Vector3 targetPosition;
    protected GameObject shroomObject;
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(SatisfyHungerGoal) });

    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }

    public void SetShroomObject(GameObject shroom)
    {
        shroomObject = shroom;
    }

    public override void OnActivated(BaseGoal linkedGoal)
    {
        base.OnActivated(linkedGoal);

        // Find the nearest shroom
        GameObject[] shrooms = GameObject.FindGameObjectsWithTag("Shroom");
        GameObject nearestShroom = FindNearestShroom(shrooms);

        // Move towards the nearest shroom using A* pathfinding
        if (nearestShroom != null)
        {
            // Set the target position for the EatShroomAction
            SetTargetPosition(nearestShroom.transform.position);

            // Set the shroom object for the EatShroomAction
            SetShroomObject(nearestShroom);

            // Move to the target position
            goapAgent.MoveTo(targetPosition);
        }
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
    }

    public override void OnTick()
    {
        // Check if the AI is already at the shroom
        if (Vector3.Distance(goapAgent.transform.position, targetPosition) <= goapAgent.reachDistance)
        {
            // AI is at the shroom, so it can eat it
            characterStats.DecreaseHunger(5);

            // Clear the shroom object from the map
            if (shroomObject != null)
            {
                if (shroomObject != null)
                {
                    ShroomManager shroomManager = FindObjectOfType<ShroomManager>();
                    shroomManager.ClearShroom(shroomObject);
                }
            }

            // Stop the agent by setting its target position to its current position
            goapAgent.MoveTo(goapAgent.transform.position);
        }
    }

    private GameObject FindNearestShroom(GameObject[] shrooms)
    {
        GameObject nearestShroom = null;
        float nearestDistance = Mathf.Infinity;
        Vector3 agentPosition = goapAgent.transform.position;

        foreach (GameObject shroom in shrooms)
        {
            float distance = Vector3.Distance(agentPosition, shroom.transform.position);
            if (distance < nearestDistance)
            {
                nearestShroom = shroom;
                nearestDistance = distance;
            }
        }

        return nearestShroom;
    }
}
