using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
-------------------------------------------
This script handles the the enemy behaviour
-------------------------------------------
*/
public class EnemyHandler : MonoBehaviour
{
    public int enemyHealth;
    public int enemyRow;
    public int enemyCol;

    void Start()
    {
        enemyHealth = 1;
        enemyRow = (int)transform.position.x;
        enemyCol = (int)transform.position.z;
    }

    void Update()
    {
        //Kill the enemy if it's health is 0
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    //Example function for moving the enemy (testing purposes)
    public void moveEnemy()
    {
        int newEnemyCol = enemyCol - 1;
        transform.position = new Vector3(enemyRow, 0.3f, newEnemyCol);
        enemyCol = newEnemyCol;
    }

    /*
    public void OnMouseDown()
    {
        moveEnemy();
    }
    */
}
