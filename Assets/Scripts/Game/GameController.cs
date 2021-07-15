using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
-------------------------------------------
This script keeps track of the gamestate
-------------------------------------------
*/
public class GameController : MonoBehaviour
{
    //Different attributes regarding the gamestate, public for testing
    public bool isPlayerTurn;
    public bool hasPlayerWon;
    public bool hasPlayerLost;

    //Player & enemy units
    public Transform playerUnits;
    public Transform enemyUnits;

    void Start()
    {
        isPlayerTurn = true;
        hasPlayerWon = false;
        hasPlayerLost = false;
        playerUnits = GameObject.Find("PlayerUnits").transform;
        enemyUnits = GameObject.Find("EnemyUnits").transform;
    }

    void Update()
    {
        //Handle enemy turn
        if (!isPlayerTurn)
        {
            foreach (Transform child in enemyUnits)
            {
                //Do stuff with the enemies
            }
            Debug.Log("Enemy turn over..");

            //Spawn new enemies

            //Switch back to players turn and replenish
            foreach (Transform child in playerUnits)
            {

            }
            isPlayerTurn = true;
        }

        //Handle ending the game, pop a win screen etc
        if (hasPlayerWon)
        {
            Debug.Log("You won!!!");
        }

        //Handle ending the game, pop a lose screen etc
        if (hasPlayerLost)
        {
            Debug.Log("You lost...");
            SceneManager.LoadScene("LoseScreen");
        }
    }

    public void ChangeTurn()
    {
        isPlayerTurn = false;
    }
}
