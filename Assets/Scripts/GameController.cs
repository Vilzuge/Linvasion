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
    //Different attributes regarding the gamestate
    public bool isPlayerTurn;
    public bool hasPlayerWon;
    public bool hasPlayerLost;

    //TEST controlling enemy
    public EnemyHandler enemy;
    public EnemySpawner enemySpawner;

    //Player & enemy units
    public Transform playerUnits;
    public Transform enemyUnits;

    void Start()
    {
        isPlayerTurn = true;
        hasPlayerWon = false;
        hasPlayerLost = false;
        enemy = GameObject.Find("Enemy").GetComponent<EnemyHandler>();
        playerUnits = GameObject.Find("PlayerUnits").transform;
        enemyUnits = GameObject.Find("EnemyUnits").transform;
        enemySpawner = GameObject.Find("EnemyUnits").GetComponent<EnemySpawner>();
    }

    void Update()
    {
        //Handle enemy turn
        if (!isPlayerTurn)
        {
            foreach (Transform child in enemyUnits)
            {
                //Do stuff with the enemies
                child.gameObject.GetComponent<EnemyHandler>().moveEnemy();
            }
            Debug.Log("Enemy turn over..");

            //Spawn new enemies
            enemySpawner.SpawnUnitWave();

            //Switch back to players turn
            foreach (Transform child in playerUnits)
            {
                //Do stuff with the enemies
                child.gameObject.GetComponent<PlayerTank>().replenishTank();
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
