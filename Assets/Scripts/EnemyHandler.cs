using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
-------------------------------------------
This script handles the the enemy behaviour
-------------------------------------------
*/
public class EnemyHandler : MonoBehaviour
{
    public Transform playerUnits;
    public GameObject gameController;

    public int enemyHealth;
    public int enemyRow;
    public int enemyCol;

    public Image healthBar;

    void Start()
    {
        enemyHealth = 2;
        enemyRow = (int)transform.position.x;
        enemyCol = (int)transform.position.z;
        playerUnits = GameObject.Find("PlayerUnits").transform;
        gameController = GameObject.Find("GameController");
    }

    void Update()
    {
        //Kill the enemy if it's health is 0
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }

        healthBar.fillAmount = enemyHealth * 0.5f;

        reachesDestination();
    }

    //Example function for moving the enemy (testing purposes)
    public void moveEnemy()
    {
        int newEnemyCol = enemyCol - 1;
        transform.position = new Vector3(enemyRow, 0.3f, newEnemyCol);
        enemyCol = newEnemyCol;
        hitsTank();
    }


    public void hitsTank()
    {
        foreach (Transform child in playerUnits)
        {
            int tankRow = child.gameObject.GetComponent<PlayerTank>().rowPos;
            int tankCol = child.gameObject.GetComponent<PlayerTank>().colPos;
            if (tankRow == enemyRow && tankCol == enemyCol)
            {
                Debug.Log("tank destroyed");
            }
        }
    }

    public void takeDamage(int damage)
    {
        enemyHealth = enemyHealth - damage;
        healthBar.fillAmount = enemyHealth * 50f;
    }

    public void reachesDestination()
    {
        if (enemyCol == 0)
        {
            gameController.GetComponent<GameController>().hasPlayerLost = true;
        }
    }
    /*
    public void OnMouseDown()
    {
        moveEnemy();
    }
    */
}
