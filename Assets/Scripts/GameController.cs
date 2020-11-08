using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

            //Switch back to players turn
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
        }
    }

    public void ChangeTurn()
    {
        isPlayerTurn = false;
    }
}
