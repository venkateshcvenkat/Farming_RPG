using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //The item information the GameObject is supposed to represent
    public itemData item;

    public virtual void Pickup()
    {
        //Set the player's inventory to the item
        InventoryManager.Instance.EquipHandSlot(item);

        //Update the changes in the scene
        InventoryManager.Instance.RenderHand();
        //Destroy this instance so as to not have multiple copies
        Destroy(gameObject);
    }
}