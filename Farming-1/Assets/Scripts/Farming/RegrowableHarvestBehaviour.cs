using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegrowableHarvestBehaviour : InteractableObject
{
    CropBehaviour parentCrop;
    public void SetParent(CropBehaviour parentCrop)
    {
        this.parentCrop = parentCrop;
    }
    public override void Pickup()
    {
        //set the player inventory to the item
        InventoryManager.Instance.EquipHandSlot(item);
        //update the changes in the scene
        InventoryManager.Instance.RenderHand();

        parentCrop.Regrow();
    }
}
