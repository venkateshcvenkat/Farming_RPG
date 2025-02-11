using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ItemSlotData
{
    public itemData itemData;
    public int quantity;

    //class constructor
    public ItemSlotData(itemData itemData, int quantity)
    {
        this.itemData = itemData;
        this.quantity = quantity;
        ValidateQuantity();
    }

    //automatically construct the class with the item data of quantity 1
    public ItemSlotData(itemData itemData)
    {
        this.itemData = itemData;
        quantity = 1;
        ValidateQuantity();
    }

    //clone the ItemSlotData
    public ItemSlotData(ItemSlotData slotToClone)
    {
        itemData = slotToClone.itemData;
        quantity = slotToClone.quantity;

    }


    //shortcut fun to add 1 to the stack
    public void AddQuantity()
    {
        AddQuantity(1);
    }
    //add a specified amount to the stack
    public void AddQuantity(int amountToAdd)
    {
        quantity += amountToAdd;
    }
    public void Remove()
    {
        quantity--;
        ValidateQuantity();
    }
    //Compare the item to see if it can be stacked
    public bool Stackable(ItemSlotData slotToCompare)
    {
        return slotToCompare.itemData == itemData;
    }
    //Do checks to see if the values make sense
    private void ValidateQuantity()
    {
        if (quantity <= 0 || itemData == null)
        {
            Empty();
        }
    }
    //empties out the item slot
    public void Empty()
    {
        itemData = null;
        quantity = 0;
    }

    //check if the slot is considered empty
    public bool IsEmpty()
    {
        return itemData == null;
    }
}
