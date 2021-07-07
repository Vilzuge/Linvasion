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
    public void SwitchToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}