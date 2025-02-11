using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment")]
public class EquipmentData : itemData
{
    public enum ToolType
    {
        Hoe,WateringCan,Axe,Pickaxe,shovel
    }
    public ToolType toolType;
}
