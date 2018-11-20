using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableMenuScrollView : MonoBehaviour {

    public GameObject scrollContent;
    public GameObject itemButtonPrefab;
    private List<InventoryItem> inventoryItems;
    private List<ConsumableMenuItem> menuItems = new List<ConsumableMenuItem>();
    private ScrollRect scrollView;
    public ConsumableInventory consumableInventory;

    // Use this for initialization
    void Start () {
        scrollView = GetComponent<ScrollRect>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void generateButtons(List<InventoryItem> items)
    {
        inventoryItems = items;
        inventoryItems.ForEach(item => {
            GameObject itemButton = Instantiate(itemButtonPrefab);
            itemButton.transform.SetParent(scrollContent.transform, false);
            ConsumableMenuItem menuItem = itemButton.GetComponent<ConsumableMenuItem>();
            menuItem.initItem(item.Data, consumableInventory, item.Quantity);
            menuItems.Add(menuItem);
        });
        scrollView.verticalNormalizedPosition = 1;
    }

    public void updateItemQuantity(InventoryItem item)
    {
        menuItems.ForEach(menuItem => {
            if(menuItem.Data == item.Data)
            {
                menuItem.updateQuantity(item.Quantity);
            }
        });
    }
}
