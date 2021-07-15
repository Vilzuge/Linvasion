using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
/*
-------------------------------------------
This script handles "back to menu" buttons
-------------------------------------------
*/
public class SceneSwitcher: MonoBehaviour
{
    //If the button is clicked, move back to menu.
    
    public void SwitchToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void SwitchToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}