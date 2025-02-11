using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationEnteryPoint : MonoBehaviour
{
    [SerializeField]
    sceneTransitionManger.Location locationToSwitch;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sceneTransitionManger.Instance.SwitchLocation(locationToSwitch);
        }
    }
}
