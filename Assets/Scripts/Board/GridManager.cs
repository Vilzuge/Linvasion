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
    [SerializeField] private Transform firstTileTransform;

    private GameObject[,] gridArray;
    public GameObject grassTile;
    public GameObject waterTile;

    void Awake()
    {
        gridArray = new GameObject[BOARD_SIZE, BOARD_SIZE];
    }
    
    
    
    void Start()
    {
        //Loading the materials
        _defaultTile = Resources.Load<Material>("Materials/GroundGreen");
        _moveableTile = Resources.Load<Material>("Materials/GroundHighlight");
        _spawnableTile = Resources.Load<Material>("Materials/GroundSpawnable");
        _shootableTile = Resources.Load<Material>("Materials/GroundShootable");
        
        GenerateBoard();
        Debug.Log("Board generated");
        
    }
    
    
    
    // Generating the gameboard
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
                    gridArray[row, col] = newTile;
                    newTile.GetComponent<BaseTile>().row = row;
                    newTile.GetComponent<BaseTile>().col = col;
                    newTile.name = $"({row},{col}) {newTile.name}";
                }
                else
                {
                    GameObject newTile = Instantiate(waterTile, new Vector3(row, -0.5f, col), Quaternion.identity, transform);
                    gridArray[row, col] = newTile;
                    newTile.GetComponent<BaseTile>().row = row;
                    newTile.GetComponent<BaseTile>().col = col;
                    newTile.name = $"({row},{col}) {newTile.name}";
                }
            }
        }
    }
    

    public void OnTileSelected(Vector3 inputPosition)
    {
        Vector2Int coordinates = CalculateCoordinatesFromPosition(inputPosition);
        Debug.Log("Row: " + coordinates.x + "Col: " + coordinates.y);
    }

    /*
    private Vector2Int CalculatePositionFromCoordinates(Vector2Int coordinates)
    {
        return 
    }
    */
    
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
