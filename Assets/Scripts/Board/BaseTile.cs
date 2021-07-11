using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
-------------------------------------------
Base tile that other tile types are extended from
-------------------------------------------
*/
public class BaseTile : MonoBehaviour
{
    public int rowIndex;
    public int colIndex;
    public bool isOccupied = false;
    public bool isWalkable;
    
    public GameObject currentOccupant;


    void Start()
    {
        
    }
    void Update()
    {

    }
}
