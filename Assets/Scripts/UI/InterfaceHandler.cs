using UnityEngine;
using UnityEngine.UI;

/*
-------------------------------------------
From old version of the game, name is dumb but was used to track unitspawning
-------------------------------------------
*/

namespace UI
{
    public class InterfaceHandler : MonoBehaviour
    {

        public int unSpawned;
        public Text text;
        public static int scoreValue = 0;
        Text score;

        void Start()
        {
            unSpawned = 3;
            GameObject.Find("ChooseSpawnText").GetComponent<Text>();
            score = GameObject.Find("Score").GetComponent<Text>();
        }

        void Update()
        {
            //If every tank is spawned, delete the tip of choosing the spawns
            if (unSpawned <= 0)
            {
                text = GameObject.Find("ChooseSpawnText").GetComponent<Text>();
                text.enabled = false;
            }

            score.text = "Score: " + scoreValue;
        }
    }
}
