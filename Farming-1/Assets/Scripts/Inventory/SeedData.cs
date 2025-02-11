using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Seed")]
public class SeedData : itemData
{
    public int daysToGrow;

    public itemData cropToYield;
    //seeding 
    public GameObject seeding;

    [Header("Regrowable")]
    //is the plant able to regrow the crop after being harvested
    public bool regrowable;
    //time taken before the plant yields another crop
    public int daysTogrow;

}
