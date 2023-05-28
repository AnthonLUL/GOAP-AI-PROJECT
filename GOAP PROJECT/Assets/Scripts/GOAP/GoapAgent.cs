using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.UI;
using System;

public class GoapAgent : MonoBehaviour
{
    private AStarPathfinder pathfinder; // Reference to the AStarPathfinder script
    public float reachDistance = 1f;
    private Dictionary<string, bool> agentState = new Dictionary<string, bool>();
    public UnityEngine.UI.Text hungerText;
    public UnityEngine.UI.Text tirednessText;
    public UnityEngine.UI.Text woodText;
    private CharacterStats characterStats;

    private void Start()
    {
        pathfinder = GetComponentInChildren<AStarPathfinder>();
        characterStats = GetComponent<CharacterStats>();
        //UnityEngine.Debug.Log("Pathfinder component: " + pathfinder);

        if (pathfinder == null)
        {
            UnityEngine.Debug.LogError("AStarPathfinder component not found.");
        }

        if (characterStats == null)
        {
            UnityEngine.Debug.LogError("CharacterStats component not found.");
        }


    }

    private void Update()
    {
        if (characterStats != null)
        {
            // Update hunger and tiredness UI elements
            hungerText.text = "Hunger: " + characterStats.hungerLevel.ToString();
            tirednessText.text = "Tiredness: " + characterStats.tirednessLevel.ToString();
            woodText.text = "Logs: " + characterStats.wood.ToString();
        }
    }


    public bool GetState(string key)
    {
        if (agentState.ContainsKey(key))
        {
            return agentState[key];
        }

        return false; // Default value if the key is not found
    }

    public void SetState(string key, bool value)
    {
        if (agentState.ContainsKey(key))
        {
            agentState[key] = value;
        }
        else
        {
            agentState.Add(key, value);
        }
    }

    // Method to initiate movement towards a target position using A* pathfinding
    public void MoveTo(Vector3 targetPosition)
    {
        // Calculate the path using the AStarPathfinder script
        pathfinder.CalculatePath(targetPosition);
    }

 
}
