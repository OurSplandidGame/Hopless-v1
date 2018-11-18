using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableInventory : MonoBehaviour {

    private ConsumableData[] itemsData;
    private List<InventoryItem> inventoryItems = new List<InventoryItem>();
    private PlayerController playerController;

	// Use this for initialization
	void Start () {
        // load static item data
        itemsData = Resources.LoadAll<ConsumableData>("Consumable Data");
        foreach (ConsumableData data in itemsData)
        {
            inventoryItems.Add(new InventoryItem(data, 0));
        }
        Debug.Log("items found: " + inventoryItems.Count);
        // load player controller
        playerController = GetComponent<PlayerController>();
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

    public void useItem(ConsumableData data)
    {
        inventoryItems.ForEach(item => 
        {
            if ((item.Data == data) && item.enoughQuantity(1))
            {
                playerController.health = (playerController.health + data.HealthChange > playerController.maxHealth) ? playerController.maxHealth : playerController.health + data.HealthChange;
                playerController.mana = (playerController.mana + data.ManaChange > playerController.maxMana) ? playerController.maxMana : playerController.mana + data.ManaChange;
                item.changeQuantiy(-1);
            }
        });
    }
}
