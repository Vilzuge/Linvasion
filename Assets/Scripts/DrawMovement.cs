using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
-------------------------------------------
OUTDATED

This script handles the visuals of movement

OUTDATED
-------------------------------------------
*/

public class DrawMovement : MonoBehaviour
{
    //Materials for different tilestates
    private static Material defaultTile;
    private static Material moveableTile;
    private static Material spawnableTile;

    void Start()
    {
        //Loading the materials
        defaultTile = Resources.Load<Material>("Materials/GroundGreen");
        moveableTile = Resources.Load<Material>("Materials/GroundHighlight");
        spawnableTile = Resources.Load<Material>("Materials/GroundSpawnable");
    }

    void Update()
    {

    }

    //Calculates the tiles where the tank can move and recolours them, requires its position and movement attribute
    public void DrawMovementGrid(int rowPos, int colPos, int moveMinttu)
    {
        for (int row = 0; row <= 5; row++)
        {
            for (int col = 0; col <= 5; col++)
            {
                if ((System.Math.Abs(rowPos - row) + System.Math.Abs(colPos - col)) <= moveMinttu)
                {
                    //Check every tile inside the gameboard change the material if moveable
                    foreach (Transform child in transform)
                    {
                        if (child.gameObject.GetComponent<TileState>().RowIndex == row && child.gameObject.GetComponent<TileState>().ColIndex == col && child.gameObject.GetComponent<TileState>().isOccupied == false)
                        {
                            child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = moveableTile;
                        }
                    }
                }
            }
        }
    }

    //Resetting the movement tiles to normal tiles
    public void ResetMovement()
    {
        Debug.Log("Palautetaan väri!");
        foreach (Transform child in transform)
        {
            child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = defaultTile;
        }
    }

    //Drawing the spawnable grid - spawning it self is handled in the CubeVisual for now
    public void DrawSpawnGrid()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<TileState>().ColIndex <= 1 && child.gameObject.GetComponent<TileState>().isOccupied == false)
            {
                child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = spawnableTile;
                child.gameObject.GetComponent<TileState>().isSpawnable = true;
            }
        }
    }

    //Drawing the spawnable grid - spawning it self is handled in the CubeVisual for now
    public void DrawLaserSpawnGrid()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<TileState>().ColIndex <= 1 && child.gameObject.GetComponent<TileState>().isOccupied == false)
            {
                child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = spawnableTile;
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
                child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = spawnableTile;
                child.gameObject.GetComponent<TileState>().isStrongSpawnable = true;
            }
        }
    }

    //Resetting the spawnable grid to normal tiles
    public void ResetSpawnGrid()
    {
        foreach (Transform child in transform)
        {
            child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = defaultTile;
            child.gameObject.GetComponent<TileState>().isSpawnable = false;
            child.gameObject.GetComponent<TileState>().isLaserSpawnable = false;
            child.gameObject.GetComponent<TileState>().isStrongSpawnable = false;
        }
    }
}
