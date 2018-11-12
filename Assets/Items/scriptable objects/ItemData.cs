using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject {
    [SerializeField]
    private string itemName;
    [SerializeField]
    private string itemDescription;
    [SerializeField]
    private Sprite itemIcon;

    public string ItemName
    {
        get
        {
            return itemName;
        }
    }

    public string ItemDescription
    {
        get
        {
            return itemDescription;
        }
    }

    public Sprite ItemIcon
    {
        get
        {
            return itemIcon;
        }
    }
}
