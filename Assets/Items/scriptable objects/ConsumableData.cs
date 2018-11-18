using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ConsumableData", menuName = "Consumable Data", order = 1)]
public class ConsumableData : ItemData {
    [SerializeField]
    private float healthChange;
    [SerializeField]
    private float manaChange;
    [SerializeField]
    private float hungerChange;

    public float HealthChange
    {
        get
        {
            return healthChange;
        }
    }

    public float ManaChange
    {
        get
        {
            return manaChange;
        }
    }

    public float HungerChange
    {
        get
        {
            return hungerChange;
        }
    }
}
