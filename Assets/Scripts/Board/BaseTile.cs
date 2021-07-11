using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
-------------------------------------------
Logic of a single tile in the gameboard
-------------------------------------------
*/
public class BaseTile : MonoBehaviour
{
    public int rowIndex;
    public int colIndex;
    public bool isOccupied;
    public bool isWalkable;
    
    public GameObject currentOccupant;


    void Start()
    {
        isOccupied = false;
    }
    void Update()
    {

    }
}
