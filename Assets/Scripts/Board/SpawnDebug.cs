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
    
    // Spawning debug units for testing purposes
    public void SpawnDebugUnits()
    {
        if (!exampleTank || !exampleEnemy) return;
        
        SpawnUnit(exampleTank, new Vector3(1f, -0.4f, 3f), false);
        SpawnUnit(exampleTank, new Vector3(3f, -0.4f, 2f), false);
        SpawnUnit(exampleTank, new Vector3(6f, -0.4f, 2f), false);
        
        SpawnUnit(exampleEnemy, new Vector3(4f, 0f, 7f), true);
        SpawnUnit(exampleEnemy, new Vector3(6f, 0f, 7f), true);
        SpawnUnit(exampleEnemy, new Vector3(3f, 0f, 6f), true);
        
    }

    
    public void SpawnUnit(GameObject unit, Vector3 position, bool isEnemy)
    {
        unit = Instantiate(unit, position, Quaternion.identity);
        
        if (isEnemy)
        {
            unit.gameObject.transform.rotation = Quaternion.identity * Quaternion.Euler(-180, 0, 0); // purkkaa, modeli vinossa
            unit.transform.SetParent(enemyUnits.transform);
        }
        else
        {
            unit.transform.SetParent(playerUnits.transform);
        }
    }
}
