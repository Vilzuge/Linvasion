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
        if (!exampleTank || !exampleEnemy) return;
        var tank = exampleTank;
        tank = Instantiate(tank, new Vector3(1f, -0.4f, 3f), Quaternion.identity);
        tank.transform.SetParent(playerUnits.transform);
        tank = Instantiate(tank, new Vector3(3f, -0.4f, 2f), Quaternion.identity);
        tank.transform.SetParent(playerUnits.transform);
        tank = Instantiate(tank, new Vector3(6f, -0.4f, 2f), Quaternion.identity);
        tank.transform.SetParent(playerUnits.transform);
                
        var enemy = exampleEnemy;
        enemy = Instantiate(enemy, new Vector3(4f, 0f, 7f), Quaternion.identity * Quaternion.Euler(-180, 0, 0));
        enemy.transform.SetParent(enemyUnits.transform);
        enemy = Instantiate(enemy, new Vector3(6f, 0f, 7f), Quaternion.identity * Quaternion.Euler(-180, 0, 0));
        enemy.transform.SetParent(enemyUnits.transform);
        enemy = Instantiate(enemy, new Vector3(3f, 0f, 6f), Quaternion.identity * Quaternion.Euler(-180, 0, 0));
        enemy.transform.SetParent(enemyUnits.transform);
    }
}
