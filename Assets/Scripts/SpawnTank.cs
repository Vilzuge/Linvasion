using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
-------------------------------------------
Handles spawning of the player's tanks
-------------------------------------------
*/
public class SpawnTank : MonoBehaviour
{
    public GameObject tankPrefab;
    public GameObject spawnedTank;
    public Transform playerUnits;
    public InterfaceHandler interfaceHandler;
    public DrawMovement movementScript;

    public int parentRow;
    public int parentCol;
    public bool canSpawn;
    // Start is called before the first frame update
    void Start()
    {

        movementScript = GameObject.Find("GameBoard").GetComponent<DrawMovement>();
        playerUnits = GameObject.Find("PlayerUnits").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseUp()
    {
        canSpawn = transform.parent.gameObject.GetComponent<TileState>().isSpawnable;
        if (canSpawn)
        {
            Spawning();
        }
    }

    //Spawn a tank in this tile if the parent is a spawnable tile
    public void Spawning()
    {
        //Getting parent's information
        parentRow = transform.parent.gameObject.GetComponent<TileState>().RowIndex;
        parentCol = transform.parent.gameObject.GetComponent<TileState>().ColIndex;

        //Spawning the tank to the coordinates and giving it correct attributes
        spawnedTank = Instantiate(tankPrefab, new Vector3(parentRow, 0.1f, parentCol), Quaternion.identity * Quaternion.Euler(-90f,0f,0f));
        spawnedTank.transform.parent = playerUnits;
        transform.parent.gameObject.GetComponent<TileState>().isOccupied = true;
        spawnedTank.GetComponent<PlayerTank>().rowPos = parentRow;
        spawnedTank.GetComponent<PlayerTank>().colPos = parentCol;

        //Changing the unspawned value of interface handler
        interfaceHandler = GameObject.Find("Canvas").GetComponent<InterfaceHandler>();
        interfaceHandler.unSpawned -= 1;

        //Resetting spawn area
        movementScript.ResetSpawnGrid();
    }
}