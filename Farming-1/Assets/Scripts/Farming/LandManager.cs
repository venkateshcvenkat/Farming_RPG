/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandManager : MonoBehaviour
{ 
    public static  LandManager Instance {  get; private set; }

    public static Tuple<List<LandSaveState>, List<CropSaveState>> farmData = null;

    public List<Land>landPlots = new List<Land>();

    //The save states of our land and crops
    public List<LandSaveState> landData = new List<LandSaveState>();
    public List<CropSaveState> cropData = new List<CropSaveState>();   
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }
    private void OnEnable()
    {
        RegisterLandPlots();
        StartCoroutine(LoadFarmData());

       
    }

    IEnumerator LoadFarmData()
    {
        yield return new WaitForEndOfFrame();
        //Load farm data if any
        if (farmData != null)
        {
            //Load in any saved data 
            ImportLandData(farmData.Item1);
            ImportCropData(farmData.Item2);
        }
    }

    private void OnDestroy()
    {
        //Save the Instance variables over to the static variable
        farmData = new Tuple<List<LandSaveState>, List<CropSaveState>>(landData, cropData);
    }
    #region Registering and Deregistering
   

    //Registers the crop onto the Instance
    public void RegisterCrop(int landID, SeedData seedToGrow, CropBehaviour.CropState cropState, int growth, int health)
    {
        cropData.Add(new CropSaveState(landID, seedToGrow.name, cropState, growth, health));
    }

    public void DeregisterCrop(int landID)
    {
        //Find its index in the list from the landID and remove it
        cropData.RemoveAll(x => x.landID == landID);
    }

    #endregion


    #region State Changes
    //Update the corresponding Land Data on ever change to the Land's state
    public void OnLandStateChange(int id, Land.LandStatus landStatus, GameTimestamp lastWatered)
    {
        landData[id] = new LandSaveState(landStatus, lastWatered);
    }

    //Update the corresponding Crop Data on ever change to the Land's state
    public void OnCropStateChange(int landID, CropBehaviour.CropState cropState, int growth, int health)
    {
        //Find its index in the list from the landID
        int cropIndex = cropData.FindIndex(x => x.landID == landID);

        string seedToGrow = cropData[cropIndex].seedToGrow;
        cropData[cropIndex] = new CropSaveState(landID, seedToGrow, cropState, growth, health);
    }
    #endregion

    #region Loading Data
    //Load over the static farmData onto the Instance's landData
    public void ImportLandData(List<LandSaveState> landDatasetToLoad)
    {
        for (int i = 0; i < landDatasetToLoad.Count; i++)
        {
            //Get the individual land save state
            LandSaveState landDataToLoad = landDatasetToLoad[i];
            //Load it up onto the Land instance
            landPlots[i].LoadLandData(landDataToLoad.landStatus, landDataToLoad.lastWatered);

        }

        landData = landDatasetToLoad;
    }

    //Load over the static farmData onto the Instance's cropData
    public void ImportCropData(List<CropSaveState> cropDatasetToLoad)
    {
        cropData = cropDatasetToLoad;
    }
    #endregion
}
*/