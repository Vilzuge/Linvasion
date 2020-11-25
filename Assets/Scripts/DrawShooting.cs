using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
-------------------------------------------
This script handles the visuals of the shootable tiles
-------------------------------------------
*/
public class DrawShooting : MonoBehaviour
{
    //Materials for different tilestates
    private static Material defaultTile;
    private static Material shootableTile;

    void Start()
    {
        //Loading the materials
        defaultTile = Resources.Load<Material>("Materials/GroundGreen");
        shootableTile = Resources.Load<Material>("Materials/GroundShootable");
    }

    void Update()
    {

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
                            child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = shootableTile;
                        }
                    }
                } else if (col == colPos && !(row == rowPos && col == colPos))
                {
                    //Check every tile inside the gameboard change the material if shootable
                    foreach (Transform child in transform)
                    {
                        if (child.gameObject.GetComponent<TileState>().RowIndex == row && child.gameObject.GetComponent<TileState>().ColIndex == col)
                        {
                            child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = shootableTile;
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
                            child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = shootableTile;
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
            child.Find("Quad").gameObject.GetComponent<MeshRenderer>().material = defaultTile;
        }
    }
}
