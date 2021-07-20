using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private GridManager grid;

    private void Awake()
    {
        grid = GetComponent<GridManager>();
    }
    
    
    void FindPath(GameObject startTile, GameObject endTile)
    {
        List<GameObject> openSet = new List<GameObject>();
        HashSet<GameObject> closedSet = new HashSet<GameObject>();
        openSet.Add(startTile);

        while (openSet.Count > 0)
        {
            
        }
    }
}
