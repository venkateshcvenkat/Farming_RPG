using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManger : MonoBehaviour
{
    public static LocationManger Instance { get; private set; }

    public  List<StartPoint> startPoints;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
       UIManager.Instance.fadeIn.SetActive(false);
    }

    //find the player's start position based on where he's coming here
    public Transform GetPlayerStartingPosition(sceneTransitionManger.Location enteringForm)
    {
        StartPoint startingPoint = startPoints.Find(x=> x.enteringFrom == enteringForm);
        return startingPoint.playerStart;
    }
}
    