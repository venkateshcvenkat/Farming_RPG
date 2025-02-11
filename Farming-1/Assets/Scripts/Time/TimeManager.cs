using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("Internal Clock")]
    [SerializeField]
    GameTimestamp timestamp;

    public float timeScale = 1.0f;

    [Header("Day/Night Cycle")]
    public Transform sunTransform;
    List<ITimeTracker> listeners = new List<ITimeTracker>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        timestamp = new GameTimestamp(0,1,6,0,GameTimestamp.Season.Spring);
        StartCoroutine(TimeUpdate() );
    }
    IEnumerator TimeUpdate()
    {
        while (true)
        {
            Tick();
            yield return new WaitForSeconds(1 / timeScale);
          
        }
    }
    public void Tick()
    {
        timestamp.UpdateClock();

        foreach(ITimeTracker listener in listeners)
        {
            listener.ClockUpdate(timestamp);
        }

       UpdateSunMovement();
    }

    public void SkipTime(GameTimestamp timeToSkipTo)
    {
        int timeToSkipInMinutes = GameTimestamp.TimestapInMinutes(timeToSkipTo);
        Debug.Log("Time to skip to :" + timeToSkipInMinutes);
        int timeNowInMinutes = GameTimestamp.TimestapInMinutes(timestamp);
        Debug.Log("Time now :" + timeNowInMinutes);

        int differenceInMiutes = timeToSkipInMinutes - timeNowInMinutes;
        Debug.Log(differenceInMiutes + "minutes will be asvances");

        //check if the timestamp to skip to has been reached
        if (differenceInMiutes <= 0) return;

        for(int i = 0; i < differenceInMiutes; i++)
        {
            Tick();
        }
    }

    public GameTimestamp GetGameTimestamp()
    {
        return new GameTimestamp(timestamp);
    }
    //day night cycle
    public void UpdateSunMovement()
    {
        int timeInMiutes = GameTimestamp.HoursToMinutes(timestamp.hour) + timestamp.minute;

        float sunAngle = 0.25f * timeInMiutes - 90;

        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }
    public void RegisterTracker(ITimeTracker listener)
    {
        listeners.Add(listener);
    }
    public void UnregisterTracker(ITimeTracker listener)
    {
        listeners.Remove(listener);
    }
}
