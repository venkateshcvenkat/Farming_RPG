/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatemanager : MonoBehaviour,ITimeTracker
{
    public static GameStatemanager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this );
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        TimeManager.Instance.RegisterTracker( this );
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
       
    }

   

}
*/