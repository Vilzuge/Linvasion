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
    public GameObject laserPrefab;
    public GameObject strongPrefab;
    public GameObject spawnedTank;
    public Transform playerUnits;
    public InterfaceHandler interfaceHandler;
    public DrawMovement movementScript;
    public SoundManagerScript soundManager;

    public int parentRow;
    public int parentCol;
    public bool canSpawn;
    public bool canSpawnLaser;
    public bool canSpawnStrong;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GameObject.Find("GameBoard").GetComponent<DrawMovement>();
        playerUnits = GameObject.Find("PlayerUnits").transform;
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseUp()
    {
        canSpawn = transform.parent.gameObject.GetComponent<TileState>().isSpawnable;
        canSpawnLaser = transform.parent.gameObject.GetComponent<TileState>().isLaserSpawnable;
        canSpawnStrong = transform.parent.gameObject.GetComponent<TileState>().isStrongSpawnable;
        if (canSpawn)
        {
            Spawning();
        }
        if (canSpawnLaser)
        {
            SpawningLaser();
        }
        if (canSpawnStrong)
        {
            SpawningStrong();
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

        //Sound of the spawning
        soundManager.PlaySound("spawn");

        //transform.parent.gameObject.GetComponent<TileState>().isOccupied = true;
        spawnedTank.GetComponent<PlayerTank>().rowPos = parentRow;
        spawnedTank.GetComponent<PlayerTank>().colPos = parentCol;

        //Changing the unspawned value of interface handler
        interfaceHandler = GameObject.Find("Canvas").GetComponent<InterfaceHandler>();
        interfaceHandler.unSpawned -= 1;

        //Resetting spawn area
        movementScript.ResetSpawnGrid();
    }

    public void SpawningLaser()
    {
        //Getting parent's information
        parentRow = transform.parent.gameObject.GetComponent<TileState>().RowIndex;
        parentCol = transform.parent.gameObject.GetComponent<TileState>().ColIndex;

        //Spawning the tank to the coordinates and giving it correct attributes
        spawnedTank = Instantiate(laserPrefab, new Vector3(parentRow, 0.1f, parentCol), Quaternion.identity * Quaternion.Euler(-90f, 0f, 0f));
        spawnedTank.transform.parent = playerUnits;

        //Sound of the spawning
        soundManager.PlaySound("spawn");

        //transform.parent.gameObject.GetComponent<TileState>().isOccupied = true;
        spawnedTank.GetComponent<LaserTank>().rowPos = parentRow;
        spawnedTank.GetComponent<LaserTank>().colPos = parentCol;

        //Changing the unspawned value of interface handler
        interfaceHandler = GameObject.Find("Canvas").GetComponent<InterfaceHandler>();
        interfaceHandler.unSpawned -= 1;

        //Resetting spawn area
        movementScript.ResetSpawnGrid();
    }

    public void SpawningStrong()
    {
        //Getting parent's information
        parentRow = transform.parent.gameObject.GetComponent<TileState>().RowIndex;
        parentCol = transform.parent.gameObject.GetComponent<TileState>().ColIndex;

        //Spawning the tank to the coordinates and giving it correct attributes
        spawnedTank = Instantiate(strongPrefab, new Vector3(parentRow, 0.1f, parentCol), Quaternion.identity * Quaternion.Euler(-90f, 0f, 0f));
        spawnedTank.transform.parent = playerUnits;

        //Sound of the spawning
        soundManager.PlaySound("spawn");

        //transform.parent.gameObject.GetComponent<TileState>().isOccupied = true;
        spawnedTank.GetComponent<StrongTank>().rowPos = parentRow;
        spawnedTank.GetComponent<StrongTank>().colPos = parentCol;

        //Changing the unspawned value of interface handler
        interfaceHandler = GameObject.Find("Canvas").GetComponent<InterfaceHandler>();
        interfaceHandler.unSpawned -= 1;

        //Resetting spawn area
        movementScript.ResetSpawnGrid();
    }
}