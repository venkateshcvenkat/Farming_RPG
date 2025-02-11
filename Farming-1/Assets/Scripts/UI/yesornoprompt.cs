using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class yesornoprompt : MonoBehaviour
{
    [Header("Sleep Prompt")]
    public GameObject sleepPanel;

    private void Start()
    {
        
        GameObject tmp = GameObject.Find("SleepPanel");
        if (tmp != null)
        {
            sleepPanel = tmp;

            sleepPanel.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sleepPanel.gameObject.SetActive(true);
        }
    }

    public void NoButton()
    {
        sleepPanel.gameObject.SetActive(false);
    }


    /* [SerializeField]
     TextMeshProUGUI promptText;
     Action onYesSelected = null;

     public static yesornoprompt instance;
     private void Awake()
     {
         instance = this;
     }

     public void CreatePrompt(string message, Action onYesSelected)
     {
         this.onYesSelected = onYesSelected;
         promptText.text = message;
     }

     public void Answer(bool yes)
     {
         if (yes && onYesSelected != null)
         {
             onYesSelected();
         }

         onYesSelected = null;
         gameObject.SetActive(false);
     }
 */
    public void Sleep()
    {

        //next day changes
        GameTimestamp timestampofNextDay = TimeManager.Instance.GetGameTimestamp();
        timestampofNextDay.day += 1;
        timestampofNextDay.hour = 6;
        timestampofNextDay.minute = 0;
        Debug.Log(timestampofNextDay.day + "" + timestampofNextDay.hour + ":" + timestampofNextDay.minute);

        TimeManager.Instance.SkipTime(timestampofNextDay);
    }
}
