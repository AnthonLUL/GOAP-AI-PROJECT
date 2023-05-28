using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatisfyHungerGoal : BaseGoal
{
    public override int CalculatePriority()
    {
        var hungerPriority = characterStats.hungerLevel * 5;
        return hungerPriority;
    }

    public override bool CanRun()
    {
        return characterStats.IsHungry();
    }
}
