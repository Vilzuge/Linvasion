using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject spawnedEnemy;
    public Transform enemyUnits;


    // Start is called before the first frame update
    void Start()
    {
        enemyUnits = GameObject.Find("EnemyUnits").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int newNumber;
    public List<int> list = new List<int>();
    public void SpawnUnitWave()
    {
        //Calculating spawn coordinates for the enemies Z=5, Y=0.3, X something in between 0 and 5
        for (int i = 0; i < 4; i++)
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


    public void SpawnUnit(int row)
    {
        //Spawning the enemy to the coordinates and giving it correct attributes
        spawnedEnemy = Instantiate(enemyPrefab, new Vector3(row, 0.3f, 5), Quaternion.identity);
        spawnedEnemy.transform.parent = enemyUnits;
        spawnedEnemy.GetComponent<EnemyHandler>().enemyRow = row;
        spawnedEnemy.GetComponent<EnemyHandler>().enemyCol = 5;
    }
}
