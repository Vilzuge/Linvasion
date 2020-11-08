using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
-------------------------------------------
Logic of a single tile in the gameboard
-------------------------------------------
*/
public class TileState : MonoBehaviour
{
    public GameObject tankPrefab;
    public bool isOccupied = false;
    public bool isSpawnable = false;
    public int RowIndex;
    public int ColIndex;
    void Start()
    {
        RowIndex = (int)transform.position.x;
        ColIndex = (int)transform.position.z;
    }
    void Update()
    {

    }


}
