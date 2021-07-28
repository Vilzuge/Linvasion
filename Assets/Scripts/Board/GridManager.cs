using System.Linq;
using Characters;
using Game;
using UnityEngine;

/*
-------------------------------------------
This script handles the game-board and it's visualization
-------------------------------------------
*/

namespace Board
{
    public class GridManager : MonoBehaviour
    {
        // Start is called before the first frame update
    
        //Materials for different tilestates
        private static Material _defaultTile;
        private static Material _moveableTile;
        private static Material _spawnableTile;
        private static Material _shootableTile;
    
    
    
        public const int BoardSize = 8;
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
            tileArray = new GameObject[BoardSize, BoardSize];
            unitArray = new GameObject[BoardSize, BoardSize];
        }
    
    
    
        void Start()
        {
            //Loading the materials
            _defaultTile = Resources.Load<Material>("Materials/GroundGreen");
            _moveableTile = Resources.Load<Material>("Materials/GroundHighlight");
            _spawnableTile = Resources.Load<Material>("Materials/GroundSpawnable");
            _shootableTile = Resources.Load<Material>("Materials/GroundShootable");
            Debug.Log("Materials loaded");
        
            //GenerateBoard();
            //Debug.Log("Board generated");
        
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
            for (int row = 0; row <= BoardSize - 1; row++)
            {
                for (int col = 0; col <= BoardSize - 1; col++)
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
            SpawnDebugTanks();
        }

        // FOR TESTING ONLY
        private void SpawnDebugTanks()
        {
            if (exampleTank)
            {
                GameObject tank = exampleTank;
                tank = Instantiate(tank, new Vector3(1f, -0.4f, 1f), Quaternion.identity, transform);
                unitArray[1, 1] = tank;
            
                tank = Instantiate(tank, new Vector3(2f, -0.4f, 3f), Quaternion.identity, transform);
                unitArray[2, 3] = tank;
            
                tank = Instantiate(tank, new Vector3(2f, -0.4f, 4f), Quaternion.identity, transform);
                unitArray[2, 4] = tank;
            }
        }
    

        public void OnTileSelected(Vector3 inputPosition)
        {
            Vector2Int coordinates = CalculateCoordinatesFromPosition(inputPosition);

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
                    DeselectUnit();
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

        public void MoveSelectedUnit(Vector2Int endCoordinates)
        {
            Vector2Int startCoordinates = selectedUnit.GetComponent<BaseTank>().position;
        
            unitArray[startCoordinates.x, startCoordinates.y] = null;
            unitArray[endCoordinates.x, endCoordinates.y] = selectedUnit;
        
            selectedUnit.transform.position = new Vector3(endCoordinates.x, -0.4f, endCoordinates.y);
            Debug.Log(selectedUnit.name + " was moved");

            // eg: move the unit, update the board, deselect unit, end turn...
        }
    
        private void SelectUnit(Vector2Int coordinates)
        {
            DeselectUnit();
            GameObject unit = GetUnitOnTile(coordinates);
            selectedUnit = unit;
            selectedUnit.GetComponent<BaseTank>().SetSelected();
            Debug.Log(selectedUnit.name + " was selected");
            // TODO: Drawing movable tiles
        }
        private void DeselectUnit()
        {
            if (!selectedUnit) return;
            Debug.Log(selectedUnit.name + " will be deselected");
            selectedUnit.GetComponent<BaseTank>().SetDeselected();
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
            if (coordinates.x < 0 || coordinates.y < 0 || coordinates.x >= BoardSize || coordinates.y >= BoardSize)
            {
                Debug.Log("Coordinates are NOT on board!!!");
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
    }
}
