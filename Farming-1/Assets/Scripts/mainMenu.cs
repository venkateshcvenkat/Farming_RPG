using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("playerHome");
        
    }
    public void Continue()
    {
        SceneManager.LoadScene("playerHome");
    }
    public void quit()
    {
        Application.Quit();
    }
}
