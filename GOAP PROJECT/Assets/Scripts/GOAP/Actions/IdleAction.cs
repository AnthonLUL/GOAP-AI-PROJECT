using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : BaseAction
{
    private float wanderRadius = 5f; // The radius within which the character will wander
    private float wanderDuration = 5f; // The duration for which the character will wander
    private float currentWanderTime = 0f; // The current wander time
    private Vector3 startingPosition; // The starting position of the character
    private Vector3 agentDestination; // The destination of the agent

    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(IdleGoal) });

    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public override void OnActivated(BaseGoal linkedGoal)
    {
        base.OnActivated(linkedGoal);

        // Store the starting position of the character
        startingPosition = goapAgent.transform.position;

        // Generate a random position within the wander radius
        Vector3 randomPosition = UnityEngine.Random.insideUnitSphere * wanderRadius;
        randomPosition += startingPosition;

        // Set the destination for the agent
        agentDestination = randomPosition;

        // Move towards the random position using A* pathfinding
        goapAgent.MoveTo(agentDestination);
    }

    public override void OnTick()
    {
        // Check if the AI has reached the agent's destination
        if (Vector3.Distance(goapAgent.transform.position, agentDestination) <= goapAgent.reachDistance)
        {
            // Increment the wander time
            currentWanderTime += Time.deltaTime;

            // Check if the wander duration is complete
            if (currentWanderTime >= wanderDuration)
            {
                // Stop the agent by setting its target position to its current position
                goapAgent.MoveTo(goapAgent.transform.position);
            }
            else
            {
                // Generate a new random position within the wander radius
                Vector3 randomPosition = UnityEngine.Random.insideUnitSphere * wanderRadius;
                randomPosition += startingPosition;

                // Set the new destination for the agent
                agentDestination = randomPosition;

                // Move towards the new random position
                goapAgent.MoveTo(agentDestination);
            }
        }
    }

    public override void OnDeactivated()
    {
        // Reset the wander time and starting position
        currentWanderTime = 0f;
        startingPosition = Vector3.zero;

        base.OnDeactivated();
    }
}
