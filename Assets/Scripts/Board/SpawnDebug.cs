using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDebug : MonoBehaviour
{
    public GameObject exampleTank;
    public GameObject exampleEnemy;
    public GameObject playerUnits;
    public GameObject enemyUnits;

    private void Start()
    {
    }

    public void SpawnDebugUnits()
    {
        if (exampleTank)
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
