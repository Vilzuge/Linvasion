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

    }

    public void ChangeTurn()
    {

    }
}
