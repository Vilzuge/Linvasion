using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInputHandler : MonoBehaviour, IInputHandler
{
    private GridManager grid;

    private void Awake()
    {
        grid = GetComponent<GridManager>();
    }
    
    public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action callback)
    {
        grid.OnTileSelected(inputPosition);
    }
}
