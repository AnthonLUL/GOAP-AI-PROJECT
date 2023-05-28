using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalSliderController : MonoBehaviour
{
    public Slider[] goalSliders; // Reference to the goal sliders in the UI
    public UnityEngine.UI.Text[] goalNameTexts; // Reference to the text components displaying the goal names
    public GOAPPlanner goapPlanner; // Reference to the GOAPPlanner script

    private void Update()
    {
        if (goapPlanner != null)
        {
            BaseGoal activeGoal = goapPlanner.ActiveGoal;
            BaseAction activeAction = goapPlanner.ActiveAction;

            // Update the sliders based on the active goal and action
            for (int i = 0; i < goalSliders.Length; i++)
            {
                Slider slider = goalSliders[i];
                UnityEngine.UI.Text goalNameText = goalNameTexts[i];

                // Get the corresponding goal type based on the goal slider's name
                string goalName = goalNameText.text;
                System.Type goalType = System.Type.GetType(goalName);

                if (activeGoal != null && activeGoal.GetType() == goalType)
                {
                    // Set the slider value based on the active goal's priority
                    slider.value = activeGoal.CalculatePriority();

                    // Set the slider colors based on the active goal and action
                    ColorBlock colors = slider.colors;
                    colors.normalColor = Color.green;
                    slider.colors = colors;
                }
                else
                {
                    // Reset the slider value and colors for non-active goals
                    slider.value = 0f;
                    slider.colors = ColorBlock.defaultColorBlock;
                }
            }
        }
    }

}
