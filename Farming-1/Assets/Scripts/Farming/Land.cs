using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour,ITimeTracker
{
   
    public enum LandStatus
    {
        Soil,Farmland,Watered
    }
    public LandStatus landStatus;
    public Material soilMat, farmlandMat, wateredMat;
    public GameObject select;

    new Renderer renderer;
    GameTimestamp timeWatered;

    [Header("Crops")]
    public GameObject cropPrefab;

    CropBehaviour cropPlanted=null;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        SwitchLandStatus(LandStatus.Soil);
        Select(false);
        TimeManager.Instance.RegisterTracker(this);
    }

    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        landStatus = statusToSwitch;
        Material materialToSwitch = soilMat;

        switch(statusToSwitch)
        {
            case LandStatus.Soil:
                materialToSwitch = soilMat;
                break;
            case LandStatus.Farmland:
                materialToSwitch = farmlandMat; 
                break;
            case LandStatus.Watered:
                materialToSwitch = wateredMat;

                timeWatered =TimeManager.Instance.GetGameTimestamp();
                break;
        }
        renderer.material = materialToSwitch;
    }
    public void Select(bool toggle)
    {
        select.SetActive(toggle);
    }
    public void InterAct()
    {
        //Check the player's tool slot
        itemData toolSlot = InventoryManager.Instance.GetEquippedSlotItem(InventorySlot.InventoryType.Tool);

        //If there's nothing equipped, return
        if (!InventoryManager.Instance.SlotEquipped(InventorySlot.InventoryType.Tool))
        {
            return;
        }
        /*if (toolSlot == null)
        {
            return;
        }*/

        EquipmentData equipmentTool = toolSlot as EquipmentData;

        if (equipmentTool != null)
        {
            EquipmentData.ToolType toolType = equipmentTool.toolType;

            switch (toolType)
            {
                case EquipmentData.ToolType.Hoe:
                    SwitchLandStatus(LandStatus.Farmland);
                    break;
                case EquipmentData.ToolType.WateringCan:
                   
                        SwitchLandStatus(LandStatus.Watered);
                    
                    break;

                case EquipmentData.ToolType.shovel:
                    if (cropPlanted != null)
                    {
                        Destroy(cropPlanted.gameObject);
                    }
                    break;
            }

            //we don't need to check for seeds if we have already confirmed the tool to be an equipment
            return;
        }

        //try casting the itemdata in the toolslot as seedData
        SeedData seedTool = toolSlot as SeedData;

        ///Condition for the player to be able to plant a seed
        ///1: He is holding a tool of type seedDate
        ///2: The land State must be either watered or farmland
        ///3: There isn't already a crop that has been planted

        if (seedTool != null && landStatus != LandStatus.Soil && cropPlanted == null)
        {
            //Instantiate the crop object parented to the land
            GameObject cropObject = Instantiate(cropPrefab, transform);
            //Move the crop object to the top of the land gameobject
            cropObject.transform.position = new Vector3(transform.position.x, 0.173f, transform.position.z);

            //Access the CropBehaviour of the crop we're going to plant
            cropPlanted = cropObject.GetComponent<CropBehaviour>();
            //Plant it with the seed's information
            cropPlanted.Plant(seedTool);

            //Consume the item
        //   InventoryManager.Instance.ConsumeItem(InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Tool));

        }
    }
   
    public void ClockUpdate(GameTimestamp timestamp)
    {
        //checked id 24hours has pssed since least watered
        if(landStatus == LandStatus.Watered)
        {
            //hours since the land was watered
            int hoursElapsed = GameTimestamp.CompareTimesstamps(timeWatered, timestamp);
            Debug.Log(hoursElapsed +"hours since this was watered");

            if(cropPlanted != null)
            {
                cropPlanted.Grow();
            }

            if(hoursElapsed > 24)
            {
                //Dry up switch back to farmland
                SwitchLandStatus(LandStatus.Farmland);
            }
        }

        if (landStatus != LandStatus.Watered && cropPlanted !=null)
        {
            if(cropPlanted.cropState != CropBehaviour.CropState.Seed)
            {
                cropPlanted.Wither();
            }
        }
    }
}
