using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int hungerLevel = 0;
    public int tirednessLevel = 0;
    public int wood = 0;

    private float hungerTimer = 0f;
    private float tirednessTimer = 0f;
    private float hungerIncreaseInterval = 3f;
    private float tirednessIncreaseInterval = 5f;

    private void Update()
    {
        UpdateHunger();
        UpdateTiredness();
    }

    private void UpdateHunger()
    {
        hungerTimer += Time.deltaTime;
        if (hungerTimer >= hungerIncreaseInterval)
        {
            IncreaseHunger(1);
            hungerTimer = 0f;
        }
    }

    private void UpdateTiredness()
    {
        tirednessTimer += Time.deltaTime;
        if (tirednessTimer >= tirednessIncreaseInterval)
        {
            IncreaseTiredness(1);
            tirednessTimer = 0f;
        }
    }

    public bool IsHungry()
    {
        return hungerLevel > 8;
    }

    public bool IsTired()
    {
        return tirednessLevel > 60;
    }

    public void IncreaseHunger(int value)
    {
        hungerLevel += value;
    }

    public void DecreaseHunger(int value)
    {
        hungerLevel -= value;
        hungerLevel = Mathf.Max(hungerLevel, 0);
    }

    public void IncreaseTiredness(int value)
    {
        tirednessLevel += value;
    }

    public void DecreaseTiredness(int value)
    {
        tirednessLevel -= value;
        tirednessLevel = Mathf.Max(tirednessLevel, 0);
    }

    public void IncreaseWood(int value)
    {
        wood += value;
    }
}



