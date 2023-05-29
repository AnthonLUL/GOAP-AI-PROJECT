using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class GoalUIController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI goalText; // Reference to the UI text element for displaying the goal

    private GOAPPlanner goapPlanner; // Reference to the GOAPPlanner script

    void Start()
    {
        goapPlanner = GameObject.FindObjectOfType<GOAPPlanner>();
    }

    void Update()
    {
        if (goapPlanner != null && goapPlanner.ActiveGoal != null)
        {
            string goalName = goapPlanner.ActiveGoal.GetType().Name;
            float priority = goapPlanner.ActiveGoal.CalculatePriority();
            goalText.text = string.Format("Active Goal: {0}\nPriority: {1}", goalName, priority);
        }
        else
        {
            goalText.text = "No Active Goal";
        }
    }
}
