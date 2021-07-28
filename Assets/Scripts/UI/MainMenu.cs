using UnityEngine;
using UnityEngine.SceneManagement;

/*
-------------------------------------------
This script handles the main menu of the game
-------------------------------------------
*/

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void QuitGame()
        {
            Debug.Log("Quit!");
            Application.Quit();
        }
    }
}
