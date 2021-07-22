using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml;
using UnityEditor;

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
    
    
    
    public const int BOARD_SIZE = 8;
    [SerializeField] private GameController controller;
    private GameObject selectedUnit;
    
    //ARRAYS OF TILES AND UNITS
    private GameObject[,] tileArray;
    private GameObject[,] unitArray;
    
    public GameObject grassTile;
    public GameObject waterTile;
    
    // FOR TESTING
    public GameObject exampleTank;
    

    void Awake()
    {
        tileArray = new GameObject[BOARD_SIZE, BOARD_SIZE];
        unitArray = new GameObject[BOARD_SIZE, BOARD_SIZE];
    }
    
    
    
    void Start()
    {
        //Loading the materials
        _defaultTile = Resources.Load<Material>("Materials/GroundGreen");
        _moveableTile = Resources.Load<Material>("Materials/GroundHighlight");
        _spawnableTile = Resources.Load<Material>("Materials/GroundSpawnable");
        _shootableTile = Resources.Load<Material>("Materials/GroundShootable");
        Debug.Log("Materials loaded");
        
        GenerateBoard();
        Debug.Log("Board generated");
        
        InitializeGame();
        Debug.Log("Game initialized");
        
    }
    
    
    
    // GENERATING GAMEBOARD
    private void GenerateBoard()
    {
        var tempList = transform.Cast<Transform>().ToList();
        foreach (var child in tempList)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
        
        // GENERATING AND SPAWNING NEW GAMEBOARD, STORED IN ARRAY
        for (int row = 0; row <= BOARD_SIZE - 1; row++)
        {
            for (int col = 0; col <= BOARD_SIZE - 1; col++)
            {
                float randomChance = Random.Range(0.0f, 1.0f);
                if (randomChance < 1.0f)
                {
                    GameObject newTile = Instantiate(grassTile, new Vector3(row, -0.5f, col), Quaternion.identity, transform);
                    tileArray[row, col] = newTile;
                    newTile.GetComponent<BaseTile>().row = row;
                    newTile.GetComponent<BaseTile>().col = col;
                    newTile.name = $"({row},{col}) {newTile.name}";
                }
                else
                {
                    GameObject newTile = Instantiate(waterTile, new Vector3(row, -0.5f, col), Quaternion.identity, transform);
                    tileArray[row, col] = newTile;
                    newTile.GetComponent<BaseTile>().row = row;
                    newTile.GetComponent<BaseTile>().col = col;
                    newTile.name = $"({row},{col}) {newTile.name}";
                }
            }
        }
    }

    private void InitializeGame()
    {
        controller = Instantiate(controller);
        controller.SetGameState(GameState.Play);
        SetDebugTank();
    }

    // FOR TESTING ONLY
    private void SetDebugTank()
    {
        if (exampleTank)
        {
            GameObject tank = exampleTank;
            tank = Instantiate(tank, new Vector3(1f, -0.4f, 1f), Quaternion.identity, transform);
            unitArray[1, 1] = tank;
        }
    }
    

    public void OnTileSelected(Vector3 inputPosition)
    {
        Vector2Int coordinates = CalculateCoordinatesFromPosition(inputPosition);
        Debug.Log("Row: " + coordinates.x + "Col: " + coordinates.y);

        if (!controller.CanPerformMove())
        {
            return;
        }
        GameObject unit = GetUnitOnTile(coordinates);
        if (selectedUnit)
        {
            if (unit != null && selectedUnit == unit)
            {
                // Deselect the unit (SELECTED UNIT IS BEING PRESSED -> DESELECT)
                DeselectUnit();
            }
            else if (unit != null && selectedUnit != unit && controller.IsTeamTurnActive())
            {
                // Select the unit (ANOTHER UNIT IS BEING PRESSED -> SELECT THE NEW ONE)
                SelectUnit(coordinates);
            }
            else if (selectedUnit.GetComponent<BaseTank>().CanMoveTo(coordinates))
            {
                // Move to position (UNIT IS SELECTED AND CAN MOVE TO THE TILE PRESSED)
                MoveSelectedUnit(coordinates);
            }
        }
        else
        {
            if (unit != null && controller.IsTeamTurnActive())
            {
                SelectUnit(coordinates);
            }
        }
    }

    public void MoveSelectedUnit(Vector2 coordinates)
    {
        selectedUnit.transform.position = new Vector3(coordinates.x, -0.4f, coordinates.y);
        Debug.Log(selectedUnit.name + " was moved");

        // eg: move the unit, update the board, deselect unit, end turn...
    }
    
    private void SelectUnit(Vector2Int coordinates)
    {
        GameObject unit = GetUnitOnTile(coordinates);
        selectedUnit = unit;
        Debug.Log(selectedUnit.name + " was selected");
        
        // TODO: Drawing movable tiles
    }
    private void DeselectUnit()
    {
        Debug.Log(selectedUnit.name + " was deselected");
        selectedUnit = null;
        
        // TODO: Calling a function to clear movable tiles that were drawn
    }

    public GameObject GetUnitOnTile(Vector2Int coordinates)
    {
        if (CheckIfCoordinatesAreOnBoard(coordinates))
        {
            return unitArray[coordinates.x, coordinates.y];
        }
        return null;
    }
    
    public bool CheckIfCoordinatesAreOnBoard(Vector2Int coordinates)
    {
        if (coordinates.x < 0 || coordinates.y < 0 || coordinates.x >= BOARD_SIZE || coordinates.y >= BOARD_SIZE)
        {
            Debug.Log("Coordinates are not on board!");
            return false;
        }
        return true;
    }

    private Vector2Int CalculateCoordinatesFromPosition(Vector3 inputPosition)
    {
        int x = Mathf.FloorToInt(transform.InverseTransformPoint(inputPosition).x + 0.5f);
        int y = Mathf.FloorToInt(transform.InverseTransformPoint(inputPosition).z + 0.5f);
        return new Vector2Int(x, y);
    }



    // DRAWING MOVEMENT
    public void DrawMovementGrid(int rowPos, int colPos, int movementValue)
    {
        Debug.Log("Coloring movable tiles.");
        for (var row = 0; row <= BOARD_SIZE-1; row++)
        {
            for (var col = 0; col <= BOARD_SIZE-1; col++)
            {
                // Check are we inside the movement radius
                if ((System.Math.Abs(rowPos - row) + System.Math.Abs(colPos - col)) <= movementValue)
                {
                    // Change the material if can be moved on
                    foreach (Transform child in transform)
                    {
                        if (child.gameObject.GetComponent<BaseTile>().row == row && child.gameObject.GetComponent<BaseTile>().col == col /* && child.gameObject.GetComponent<BaseTile>().isOccupied == false */)
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

    }
    
    // DRAW SPAWNING GRID FOR THE SELECTED TANK
    public void DrawSpawnGrid(string tankType)
    {

    }

    public void ResetSpawnGrid()
    {

    }
    
    
    //Calculates the tiles where the tank can move and recolours them, requires its position and movement attribute
    public void DrawShootingGrid(int rowPos, int colPos)
    {

    }

    public void DrawStrongShootingGrid(int rowPos, int colPos)
    {

    }

    //Resets the shooting grid back to normal tiles
    public void ResetShootingGrid()
    {

    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
