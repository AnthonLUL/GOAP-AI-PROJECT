using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepGoal : BaseGoal
{
    public override int CalculatePriority()
    {
        var sleepPriority = characterStats.tirednessLevel * 5;
        return sleepPriority;
    }

    public override bool CanRun()
    {
        return characterStats.IsTired();
    }
}
