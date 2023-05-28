using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepAction : BaseAction
{
    protected Vector3 targetPosition;
    protected GameObject bedObject;
    private float sleepTime = 10f; // Sleep time in seconds
    private float currentSleepTime = 0f; // Current sleep time

    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(SleepGoal) });

    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }

    public void SetBedObject(GameObject bed)
    {
        bedObject = bed;
    }

    public override void OnActivated(BaseGoal linkedGoal)
    {
        base.OnActivated(linkedGoal);

        // Find the bed object
        GameObject bed = GameObject.FindGameObjectWithTag("Bed");

        // Move towards the bed using A* pathfinding
        if (bed != null)
        {
            // Set the target position for the SleepAction
            SetTargetPosition(bed.transform.position);

            // Set the bed object for the SleepAction
            SetBedObject(bed);

            // Move to the target position
            goapAgent.MoveTo(targetPosition);
        }
    }

    public override void OnDeactivated()
    {
        // Clear the bed object if the action is interrupted or completed
        bedObject = null;

        base.OnDeactivated();
    }

    public override void OnTick()
    {
        // Check if the AI is already at the bed
        if (Vector3.Distance(goapAgent.transform.position, targetPosition) <= goapAgent.reachDistance)
        {
            // AI is at the bed, so it can sleep
            currentSleepTime += Time.deltaTime;

            if (currentSleepTime >= sleepTime)
            {
                // Sleep time is complete
                characterStats.DecreaseTiredness(100);

                // Stop the agent by setting its target position to its current position
                goapAgent.MoveTo(goapAgent.transform.position);
            }
        }
    }
}
