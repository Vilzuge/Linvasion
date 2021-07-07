using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
-------------------------------------------
This script handles the game-board and it's visualization
-------------------------------------------
*/

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    //Materials for different tilestates
    private static Material _defaultTile;
    private static Material _moveableTile;
    private static Material _spawnableTile;
    private static Material _shootableTile;
    void Start()
    {
        //Loading the materials
        _defaultTile = Resources.Load<Material>("Materials/GroundGreen");
        _moveableTile = Resources.Load<Material>("Materials/GroundHighlight");
        _spawnableTile = Resources.Load<Material>("Materials/GroundSpawnable");
        _shootableTile = Resources.Load<Material>("Materials/GroundShootable");
    }

    public void DrawMovementGrid(int rowPos, int colPos, int moveMinttu)
    {
        for (var row = 0; row <= 5; row++)
        {
            for (var col = 0; col <= 5; col++)
            {
                if ((System.Math.Abs(rowPos - row) + System.Math.Abs(colPos - col)) <= moveMinttu)
                {
                    //Check every tile inside the gameboard change the material if moveable
                    foreach (Transform child in transform)
                    {
                        if (child.gameObject.GetComponent<TileState>().RowIndex == row && child.gameObject.GetComponent<TileState>().ColIndex == col && child.gameObject.GetComponent<TileState>().isOccupied == false)
                        {
                            child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = _moveableTile;
                        }
                    }
                }
            }
        }
    }
    
    //Resetting tiles after drawing movement
    public void ResetMovement()
    {
        Debug.Log("Palautetaan väri!");
        foreach (Transform child in transform)
        {
            child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = _defaultTile;
        }
    }
    
    public void DrawSpawnGrid()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<TileState>().ColIndex <= 1 && child.gameObject.GetComponent<TileState>().isOccupied == false)
            {
                child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = _spawnableTile;
                child.gameObject.GetComponent<TileState>().isSpawnable = true;
            }
        }
    }
    
    public void DrawLaserSpawnGrid()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<TileState>().ColIndex <= 1 && child.gameObject.GetComponent<TileState>().isOccupied == false)
            {
                child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = _spawnableTile;
                child.gameObject.GetComponent<TileState>().isLaserSpawnable = true;
            }
        }
    }

    public void DrawStrongSpawnGrid()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<TileState>().ColIndex <= 1 && child.gameObject.GetComponent<TileState>().isOccupied == false)
            {
                child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = _spawnableTile;
                child.gameObject.GetComponent<TileState>().isStrongSpawnable = true;
            }
        }
    }
    
    public void ResetSpawnGrid()
    {
        foreach (Transform child in transform)
        {
            child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = _defaultTile;
            child.gameObject.GetComponent<TileState>().isSpawnable = false;
            child.gameObject.GetComponent<TileState>().isLaserSpawnable = false;
            child.gameObject.GetComponent<TileState>().isStrongSpawnable = false;
        }
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
