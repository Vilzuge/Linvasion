using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameObject spawnedEnemy;
    private Transform enemyUnits;

    //Attributes for calculating spawning positions
    private int newNumber;
    public List<int> list = new List<int>();

    void Start()
    {
        //Finding the enemies parent
        enemyUnits = GameObject.Find("EnemyUnits").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnUnitWave()
    {
        //Calculating spawn coordinates for the enemies Z=5, Y=0.3, X=? something in between 0 and 5
        for (int i = 0; i < 5; i++)
        {
            newNumber = Random.Range(0, 6);
            if (!list.Contains(newNumber)) {
                list.Add(newNumber);
            }
        }

        foreach(int coordinate in list)
        {
            SpawnUnit(coordinate);
        }

        list.Clear();
    }

    //Spawning the enemy to the coordinates and giving it correct attributes
    public void SpawnUnit(int row)
    {
        spawnedEnemy = Instantiate(enemyPrefab, new Vector3(row, 0.3f, 5), Quaternion.identity * Quaternion.Euler(-90f, 0f, -90f));
        spawnedEnemy.transform.parent = enemyUnits;
        spawnedEnemy.GetComponent<EnemyHandler>().enemyRow = row;
        spawnedEnemy.GetComponent<EnemyHandler>().enemyCol = 5;
    }
}
