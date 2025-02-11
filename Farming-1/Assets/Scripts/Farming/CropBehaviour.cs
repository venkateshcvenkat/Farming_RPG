using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehaviour : MonoBehaviour
{
    //information on what the crop will grow
    SeedData seedToGrow;

    [Header("Stages of Life")]
    public GameObject seed;
    public GameObject wilted;
    private GameObject seedling;
    private GameObject harvestable;

    //current stage in the crop growth
    public CropState cropState;

    int growth;
    int maxGrowth;

    int maxHealth = GameTimestamp.HoursToMinutes(48);
    int health;
    public enum CropState
    {
        Seed,Seedling,Harvestable,wilted
    }
    public void Plant(SeedData seedToGrow)
    {
        this.seedToGrow = seedToGrow;

        //instantiate the seedling and harvestable ganeobjects
        seedling = Instantiate(seedToGrow.seeding, transform);

        //Acess the crop itemdata
        itemData cropToYield = seedToGrow.cropToYield;

        //Instantiate the harvestable crop
        harvestable = Instantiate(cropToYield.gameModel, transform);
        //convert days to grow into houra
        int hoursTOGrow = GameTimestamp.DaysToHours(seedToGrow.daysToGrow);
        //convert it to minutes
        maxGrowth = GameTimestamp.HoursToMinutes(hoursTOGrow);

        //check if it is regrowable
        if (seedToGrow.regrowable)
        {
            RegrowableHarvestBehaviour regrowableHarvest = harvestable.GetComponent<RegrowableHarvestBehaviour>();
            regrowableHarvest.SetParent(this);    
        }

        //set the intial state to seed
        SwitchState(CropState.Seed);
       
    }
    public void Grow()
    {
        growth++;

        if(health < maxHealth)
        {
            health++;
        }

        //the seed will sprout into a seedling when the growth is at 50%
        if (growth >= maxGrowth/2 && cropState == CropState.Seed)
        {
            SwitchState(CropState.Seedling);
        }

        //Grow from seedling to harveatable
        if (growth >= maxGrowth && cropState == CropState.Seedling)
        {
            SwitchState(CropState.Harvestable);
        }
    }

    public void Wither()
    {
        health--;

        if(health <=0 && cropState != CropState.Seed)
        {
            SwitchState(CropState.wilted);
        }
    }

    void SwitchState(CropState stateToSwitch)
    {
        seed.SetActive(false);
        seedling.SetActive(false);
        harvestable.SetActive(false);
        wilted.SetActive(false);

        switch (stateToSwitch)
        {
            case CropState.Seed:
                seed.SetActive(true);
                break;
            case CropState.Seedling:

                seedling.SetActive(true);
                health = maxHealth;

                break;
            case CropState.Harvestable:
                harvestable.SetActive(true);

                if(!seedToGrow.regrowable)
                {
                    //unparent it the crop
                    harvestable.transform.parent = null;
                    Destroy(gameObject);
                }
                break;
            case CropState.wilted:
                wilted.SetActive(true);
               
                break;
        }
        //set the current crop state to the wr're switching to
        cropState = stateToSwitch;
    }

    public void Regrow()
    {
        int hourToRegrow = GameTimestamp.DaysToHours(seedToGrow.daysToGrow);
        growth=maxGrowth - GameTimestamp.HoursToMinutes(hourToRegrow);

        SwitchState(CropState.Seedling);
    }
}
