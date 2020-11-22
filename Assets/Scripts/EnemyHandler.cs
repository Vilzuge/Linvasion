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
    //Objects needed for the enemy handling
    private Transform playerUnits;
    private GameObject gameController;

    //Enemy attributes, some are public for testing reasons
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
            //PLAY A SOUND HERE
            InterfaceHandler.scoreValue += 100;
            Destroy(gameObject);
        }

        healthBar.fillAmount = enemyHealth * 0.5f;

        //Check if reached the backline
        reachesDestination();
    }

    //Moving the enemy closer to backline
    public void moveEnemy()
    {
        int newEnemyCol = enemyCol - 1;
        transform.position = new Vector3(enemyRow, 0.3f, newEnemyCol);
        enemyCol = newEnemyCol;
        hitsTank();
    }


    //Check if reached a tank
    public void hitsTank()
    {
        foreach (Transform child in playerUnits)
        {
            int tankRow = child.gameObject.GetComponent<PlayerTank>().rowPos;
            int tankCol = child.gameObject.GetComponent<PlayerTank>().colPos;
            if (tankRow == enemyRow && tankCol == enemyCol)
            {
                //Do something here when the enemy hits the tank
                Debug.Log("tank destroyed");
            }
        }
    }

    //Taking damage from the player, and updating health bar
    public void takeDamage(int damage)
    {
        enemyHealth = enemyHealth - damage;
        healthBar.fillAmount = enemyHealth * 50f;
    }

    //Check if enemy reached the backline
    public void reachesDestination()
    {
        if (enemyCol == 0)
        {
            gameController.GetComponent<GameController>().hasPlayerLost = true;
        }
    }
}
