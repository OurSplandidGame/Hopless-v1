using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableInventory : MonoBehaviour {

    private ConsumableData[] itemsData;
    private List<InventoryItem> inventoryItems = new List<InventoryItem>();
    private PlayerController playerController;
    public ConsumableMenuScrollView menu;

    public List<InventoryItem> InventoryItems
    {
        get
        {
            return inventoryItems;
        }
    }

    public int MpPotion
    {
        get
        {
            int res = 0;
            inventoryItems.ForEach(item =>
            {
                if ((item.Data.name == "Large Mana Potion"))
                {
                    res = item.Quantity;
                }
            });
            return res;
        }
    }

    public int HpPotion
    {
        get
        {
            int res = 0;
            inventoryItems.ForEach(item =>
            {
                if ((item.Data.name == "Large Health Potion"))
                {
                    res = item.Quantity;
                }
            });
            return res;
        }
    }

    public void ConsumeHpPotion(int num)
    {
        inventoryItems.ForEach(item =>
        {
            if ((item.Data.name == "Large Health Potion") && item.enoughQuantity(num))
            {
                item.changeQuantiy(-num);
                menu.updateItemQuantity(item);
            }
        });
    }

    public void ConsumeMpPotion(int num)
    {
        inventoryItems.ForEach(item =>
        {
            if ((item.Data.name == "Large Mana Potion") && item.enoughQuantity(num))
            {
                item.changeQuantiy(-num);
                menu.updateItemQuantity(item);
            }
        });
    }

    // Use this for initialization
    void Start () {
        // load static item data
        itemsData = Resources.LoadAll<ConsumableData>("Consumable Data");
        foreach (ConsumableData data in itemsData)
        {
            inventoryItems.Add(new InventoryItem(data, 0));
        }
        menu.generateButtons(inventoryItems);
        Debug.Log("items found: " + inventoryItems.Count);
        // load player controller
        playerController = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        /*inventoryItems.ForEach(item =>
        {
            Debug.Log("Item " + item.Data.ItemName + " : " + item.Quantity);
        });*/
    }

    public void addToInventory(ConsumableData data)
    {
        inventoryItems.ForEach(item =>
        {
            if (item.Data == data)
            {
                item.changeQuantiy(1);
                menu.updateItemQuantity(item);
            }
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
                menu.updateItemQuantity(item);
            }
        });
    }
}
