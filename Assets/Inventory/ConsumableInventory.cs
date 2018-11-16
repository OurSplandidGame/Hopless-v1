using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableInventory : MonoBehaviour {

    ConsumableData[] itemsData;
    List<InventoryItem> inventoryItems = new List<InventoryItem>();

	// Use this for initialization
	void Start () {
        itemsData = Resources.LoadAll<ConsumableData>("Consumable Data");
        foreach (ConsumableData data in itemsData)
        {
            inventoryItems.Add(new InventoryItem(data, 0));
        }
        Debug.Log("items found: " + inventoryItems.Count);
    }
	
	// Update is called once per frame
	void Update ()
    {
        inventoryItems.ForEach(item =>
        {
            Debug.Log("Item " + item.Data.ItemName + " : " + item.Quantity);
        });
    }

    public void addToInventory(ConsumableData data)
    {
        inventoryItems.ForEach(item =>
        {
            if (item.Data == data)
                item.changeQuantiy(1);
        });
    }
}
