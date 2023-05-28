using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGoal : MonoBehaviour
{
    protected BaseAction LinkedAction;
    protected CharacterStats characterStats;
    protected GoapAgent goapAgent;

    void Awake()
    {
        characterStats = GetComponent<CharacterStats>();
        goapAgent = GetComponent<GoapAgent>();
    }

    public virtual int CalculatePriority()
    {
        return -1; // Return a default priority value
    }

    public virtual bool CanRun()
    {
        return false; // Return false by default
    }

    public virtual void OnGoalActivated(BaseAction linkedAction)
    {
        LinkedAction = linkedAction;
    }

    public virtual void OnGoalDeactivated()
    {
        LinkedAction = null;
    }

    public virtual void OnTickGoal()
    {

    }
}
