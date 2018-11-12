using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ConsumableData", menuName = "Consumable Data", order = 1)]
public class ConsumableData : ItemData {
    [SerializeField]
    private int healthChange;
    [SerializeField]
    private int manaChange;
    [SerializeField]
    private int hungerChange;

    public int HealthChange
    {
        get
        {
            return healthChange;
        }
    }

    public int ManaChange
    {
        get
        {
            return manaChange;
        }
    }

    public int HungerChange
    {
        get
        {
            return hungerChange;
        }
    }
}
