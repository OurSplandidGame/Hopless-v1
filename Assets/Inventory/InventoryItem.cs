using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem {
    private ItemData itemData;
    private int quantity;

    public InventoryItem(ItemData input_item, int input_quantity = 0)
    {
        itemData = input_item;
        quantity = input_quantity;
    }

    public ItemData Data
    {
        get
        {
            return itemData;
        }
    }

    public int Quantity
    {
        get
        {
            return quantity;
        }
    }

    public bool enoughQuantity (int quantityValue)
    {
        return (quantity >= quantityValue);
    }

    private void _changeQuantity (int changeValue)
    {
        quantity += changeValue;
        if (quantity < 0)
            quantity = 0;
    }

    public bool changeQuantiy(int changeValue)
    {
        if (!enoughQuantity(-changeValue))
            return false;
        _changeQuantity(changeValue);
        return true;
    }
}
