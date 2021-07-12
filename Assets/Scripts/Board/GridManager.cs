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

    public int dimension;
    
    void Start()
    {
        //Loading the materials
        _defaultTile = Resources.Load<Material>("Materials/GroundGreen");
        _moveableTile = Resources.Load<Material>("Materials/GroundHighlight");
        _spawnableTile = Resources.Load<Material>("Materials/GroundSpawnable");
        _shootableTile = Resources.Load<Material>("Materials/GroundShootable");
    }

    // DRAWING MOVEMENT
    public void DrawMovementGrid(int rowPos, int colPos, int movementValue)
    {
        Debug.Log("Coloring movable tiles.");
        for (var row = 0; row <= dimension-1; row++)
        {
            for (var col = 0; col <= dimension-1; col++)
            {
                // Check are we inside the movement radius
                if ((System.Math.Abs(rowPos - row) + System.Math.Abs(colPos - col)) <= movementValue)
                {
                    // Change the material if can be moved on
                    foreach (Transform child in transform)
                    {
                        if (child.gameObject.GetComponent<BaseTile>().rowIndex == row && child.gameObject.GetComponent<BaseTile>().colIndex == col && child.gameObject.GetComponent<BaseTile>().isOccupied == false)
                        {
                            child.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = _moveableTile;
                        }
                    }
                }
            }
        }
    }
    
    // RESET MOVEMENT DRAW
    public void ResetMovement()
    {
        Debug.Log("Palautetaan väri!");
        foreach (Transform child in transform)
        {
            child.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = _defaultTile;
        }
    }
    
    // DRAW SPAWNING GRID FOR THE SELECTED TANK
    public void DrawSpawnGrid(string tankType)
    {
        foreach (Transform child in transform)
        {
            // Assigning the spawn area happens here
            if (child.gameObject.GetComponent<TileState>().ColIndex <= 1 && child.gameObject.GetComponent<TileState>().isOccupied == false)
            {
                child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = _spawnableTile;
                child.gameObject.GetComponent<TileState>().isSpawnable = true;
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
    
    
    //Calculates the tiles where the tank can move and recolours them, requires its position and movement attribute
    public void DrawShootingGrid(int rowPos, int colPos)
    {
        for (int row = 0; row <= 5; row++)
        {
            for (int col = 0; col <= 5; col++)
            {
                if (row == rowPos && !(row == rowPos && col == colPos))
                {
                    //Check every tile inside the gameboard change the material if shootable
                    foreach (Transform child in transform)
                    {
                        if (child.gameObject.GetComponent<TileState>().RowIndex == row && child.gameObject.GetComponent<TileState>().ColIndex == col)
                        {
                            child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = _shootableTile;
                        }
                    }
                } else if (col == colPos && !(row == rowPos && col == colPos))
                {
                    //Check every tile inside the gameboard change the material if shootable
                    foreach (Transform child in transform)
                    {
                        if (child.gameObject.GetComponent<TileState>().RowIndex == row && child.gameObject.GetComponent<TileState>().ColIndex == col)
                        {
                            child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = _shootableTile;
                        }
                    }
                }
            }
        }
    }

    public void DrawStrongShootingGrid(int rowPos, int colPos)
    {
        for (int row = 0; row <= 5; row++)
        {
            for (int col = 0; col <= 5; col++)
            {
                if (row == rowPos && !(row == rowPos && col == colPos))
                {
                    //Check every tile inside the gameboard change the material if shootable
                    foreach (Transform child in transform)
                    {
                        if (child.gameObject.GetComponent<TileState>().RowIndex == row && child.gameObject.GetComponent<TileState>().ColIndex == col)
                        {
                            child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = _shootableTile;
                        }
                    }
                }
            }
        }
    }

    //Resets the shooting grid back to normal tiles
    public void ResetShootingGrid()
    {
        Debug.Log("Palautetaan väri!");
        foreach (Transform child in transform)
        {
            child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = _defaultTile;
        }
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
