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
    //public GameObject occupier;
    //public bool isOccupied = false;
    //public bool isSpawnable = false;
    //public bool isLaserSpawnable = false;
    //public bool isStrongSpawnable = false;

    public int rowIndex;
    public int colIndex;
    
    void Start()
    {
        rowIndex = (int)transform.position.x;
        colIndex = (int)transform.position.z;
    }
    void Update()
    {

    }
}
