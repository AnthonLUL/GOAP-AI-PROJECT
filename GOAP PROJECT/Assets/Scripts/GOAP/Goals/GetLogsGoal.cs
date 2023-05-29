using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLogsGoal : BaseGoal
{
    public int Priority = 60;
    public override int CalculatePriority()
    {
        return Priority;
    }

    public override bool CanRun()
    {
        return true;
    }
}
