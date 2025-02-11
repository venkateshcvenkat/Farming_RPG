using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransitionManger : MonoBehaviour
{
    public static sceneTransitionManger Instance;
    public enum Location { Farming ,playerHome,Town}

    public Location currentLocation;

    Transform playerPoint;

    //Check if the screen has finished fading out
    bool screenFadedOut;

    private void Awake()
    {
        if( Instance != null && Instance != this )
        {
            Destroy(gameObject); 
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnLocationLoad;

        playerPoint = FindObjectOfType<PlayerController>().transform;
    }

    public void SwitchLocation(Location locationToSwitch)
    {
        //  SceneManager.LoadScene(locationToSwitch.ToString());
        UIManager.Instance.FadeOutScreen();
        screenFadedOut = false;
     
        StartCoroutine(ChangeScene(locationToSwitch));
    }

    IEnumerator ChangeScene(Location locationToSwitch)
    {
        //Disable the player's CharacterController component
        CharacterController playerCharacter = playerPoint.GetComponent<CharacterController>();
        playerCharacter.enabled = false;
        //Wait for the scene to finish fading out before loading the next scene
        while (!screenFadedOut)
        {
            yield return new WaitForSeconds(0.1f);
        }

        //Reset the boolean
        screenFadedOut = false;
        UIManager.Instance.ResetFadeDefaults();
        SceneManager.LoadScene(locationToSwitch.ToString());

    }

    //Called when the screen has faded out
    public void OnFadeOutComplete()
    {
        screenFadedOut = true;

    }




    public void OnLocationLoad(Scene scene,LoadSceneMode mode)
    {
        Location oldLocation = currentLocation;

        Location newLocation = (Location)Enum.Parse(typeof(Location), scene.name);

        if (currentLocation == newLocation) return;

        Transform startPoint = LocationManger.Instance.GetPlayerStartingPosition(oldLocation);

        CharacterController playerController = playerPoint.GetComponent<CharacterController>();
        playerController.enabled = false;

        playerPoint.position = startPoint.position;
        playerPoint.rotation = startPoint.rotation;

        playerController.enabled = true;

        currentLocation = newLocation;
    }
}
