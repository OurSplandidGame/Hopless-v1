using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableMenuItem : MonoBehaviour {
    
    private Text quantityText;
    private ItemData itemData;
    private int quantity = 0;
    private Image image;
    private Button button;
    private ConsumableInventory consumableInventory;

    public ItemData Data
    {
        get
        {
            return itemData;
        }
    }

    // Use this for initialization
    void Start () {
        
        //GameObject itemQuantity = gameObject.transform.Find("ItemQuantity").gameObject;// quantityDisplay.GetComponent<Text>();
        //quantityText = itemQuantity.GetComponent<Text>();
	}

    private void Awake()
    {
        image = GetComponent<Image>();
        GameObject itemQuantity = gameObject.transform.Find("ItemQuantity").gameObject;
        quantityText = itemQuantity.GetComponent<Text>();
        button = GetComponent<Button>();
        button.onClick.AddListener(clickHandler);
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void _updateImage()
    {
        image.sprite = itemData.ItemIcon;
    }

    private void _updateQuantity()
    {
        quantityText.text = quantity.ToString();
    }

    private void _updateAll()
    {
        this._updateQuantity();
        this._updateImage();
    }

    public void initItem(ItemData data, ConsumableInventory inventory, int qty = 0)
    {
        consumableInventory = inventory;
        itemData = data;
        quantity = qty;
        this._updateAll();
    }

    public void updateQuantity(int qty)
    {
        quantity = qty;
        this._updateQuantity();
    }

    private void clickHandler()
    {
        consumableInventory.useItem((ConsumableData)itemData);
    }
}
