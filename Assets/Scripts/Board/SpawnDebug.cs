using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
-------------------------------------------
This script spawns units for testing purposes before spawning is implemented
-------------------------------------------
*/

public class SpawnDebug : MonoBehaviour
{
    public GameObject exampleTank;
    public GameObject exampleEnemy;
    public GameObject playerUnits;
    public GameObject enemyUnits;
    
    public void SpawnDebugUnits()
    {
        if (exampleTank && exampleEnemy)
        {
            GameObject tank = exampleTank;
            tank = Instantiate(tank, new Vector3(1f, -0.4f, 1f), Quaternion.identity);
            tank.transform.SetParent(playerUnits.transform);
            tank = Instantiate(tank, new Vector3(2f, -0.4f, 2f), Quaternion.identity);
            tank.transform.SetParent(playerUnits.transform);
            tank = Instantiate(tank, new Vector3(5f, -0.4f, 0f), Quaternion.identity);
            tank.transform.SetParent(playerUnits.transform);
                
            GameObject enemy = exampleEnemy;
            enemy = Instantiate(enemy, new Vector3(4f, -0.4f, 7f), Quaternion.identity * Quaternion.Euler(-90, 0, -90));
            enemy.transform.SetParent(enemyUnits.transform);
            enemy = Instantiate(enemy, new Vector3(6f, -0.4f, 7f), Quaternion.identity * Quaternion.Euler(-90, 0, -90));
            enemy.transform.SetParent(enemyUnits.transform);
            enemy = Instantiate(enemy, new Vector3(3f, -0.4f, 6f), Quaternion.identity * Quaternion.Euler(-90, 0, -90));
            enemy.transform.SetParent(enemyUnits.transform);
        }
    }
}