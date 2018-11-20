using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableMenu : MonoBehaviour {

    public GameObject buttonPrefab;
    private List<InventoryItem> inventoryItems;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void initButtons(List<InventoryItem> items)
    {
        inventoryItems = items;
        inventoryItems.ForEach(item => {
            GameObject itemButton = Instantiate(buttonPrefab) as GameObject;
            itemButton.GetComponent<Image>().sprite = item.Data.ItemIcon;
            itemButton.transform.SetParent(gameObject.transform);
            itemButton.transform.position = gameObject.transform.position;
            Debug.Log(gameObject.transform);
        });
        Debug.Log("Items in menu: " + inventoryItems.Count);
    }
}
