using UnityEngine;
using UnityEngine.SceneManagement;

/*
-------------------------------------------
This script handle scene switches in the game
-------------------------------------------
*/
namespace Game
{
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
}