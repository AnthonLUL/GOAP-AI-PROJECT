using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected BaseGoal linkedGoal;
    protected CharacterStats characterStats;
    protected GoapAgent goapAgent;
    protected GOAPPlanner goapPlanner;
    protected bool isActionComplete;

    protected virtual void Awake()
    {
        characterStats = GetComponent<CharacterStats>();
        goapAgent = GetComponent<GoapAgent>();
        goapPlanner = GetComponent<GOAPPlanner>();
        isActionComplete = false;
    }

    public virtual List<System.Type> GetSupportedGoals()
    {
        return new List<System.Type>(); // Return an empty list by default
    }

    public virtual bool CanRun()
    {
        return true; // Return true by default
    }

    public virtual float GetCost()
    {
        return 0f; // Return a cost of 0 by default
    }

    public virtual void OnActivated(BaseGoal linkedGoal)
    {
        this.linkedGoal = linkedGoal;
        isActionComplete = false;
    }

    public virtual void OnDeactivated()
    {
        linkedGoal = null;
        isActionComplete = false;
    }

    public virtual void OnTick()
    {
        // Implement the action's behavior
    }

    public bool IsActionComplete()
    {
        return isActionComplete;
    }
}
