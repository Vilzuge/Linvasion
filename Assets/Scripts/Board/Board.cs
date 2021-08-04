using System.Linq;
using System.Collections.Generic;
using Characters;
using Game;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/*
-------------------------------------------
This script handles the game-board and it's visualization
-------------------------------------------
*/

namespace Board
{
    public class Board : MonoBehaviour
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
        private BaseTile[,] tileArray;

        public GameObject playerUnits;
        public GameObject enemyUnits;

        public List<BaseTile> path;
    
        public GameObject grassTile;
        public GameObject waterTile;
    
        // FOR TESTING
        public GameObject exampleTank;
        public GameObject exampleEnemy;
    

        void Awake()
        {
            tileArray = new BaseTile[BoardSize, BoardSize];
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

        void Update()
        {
            /*
             * GameObject hoverUnit = GetUnitOnTile(hoverPosition)
             * if (hoverUnit && hoverUnit.GetComponent<BaseTank>().hasMove)
             *      TryDrawUnitMovement(GameObject hoverUnit;
             * else
             * {
             *      ClearBoardMovement()
             * }
             */
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
                    if (randomChance < 0.8f)
                    {
                        GameObject newTile = Instantiate(grassTile, new Vector3(row, -0.5f, col), Quaternion.identity, transform);
                        TileGrass component = newTile.AddComponent<TileGrass>();
                        component.walkable = true;
                        component.worldPosition = new Vector3(row, -0.5f, col);
                        component.gridX = row;
                        component.gridY = col;
                        tileArray[row, col] = newTile.GetComponent<TileGrass>();
                        newTile.name = $"({row},{col}) {newTile.name}";
                    }
                    else
                    {
                        GameObject newTile = Instantiate(waterTile, new Vector3(row, -0.5f, col), Quaternion.identity, transform);
                        TileWater component = newTile.AddComponent<TileWater>();
                        component.walkable = false;
                        component.worldPosition = new Vector3(row, -0.5f, col);
                        component.gridX = row;
                        component.gridY = col;
                        tileArray[row, col] = newTile.GetComponent<TileWater>();
                        newTile.name = $"({row},{col}) {newTile.name}";
                    }
                }
            }
        }

        private void InitializeGame()
        {
            controller = Instantiate(controller);
            controller.SetGameState(GameState.Play);
            SpawnDebugUnits();
        }

        // FOR TESTING WITHOUT SPAWNING
        private void SpawnDebugUnits()
        {
            if (exampleTank)
            {
                GameObject tank = exampleTank;
                tank = Instantiate(tank, new Vector3(1f, -0.4f, 1f), Quaternion.identity);
                tank.transform.SetParent(playerUnits.transform);
                tank = Instantiate(tank, new Vector3(2f, -0.4f, 2f), Quaternion.identity);
                tank.transform.SetParent(playerUnits.transform);
                tank = Instantiate(tank, new Vector3(5f, -0.4f, 0f), Quaternion.identity);
                tank.transform.SetParent(playerUnits.transform);
                
                GameObject enemy = exampleEnemy;
                enemy = Instantiate(enemy, new Vector3(4f, -0.4f, 7f), Quaternion.identity * Quaternion.Euler(-90, 0, -90));
                enemy.transform.SetParent(enemyUnits.transform);
                enemy = Instantiate(enemy, new Vector3(6f, -0.4f, 7f), Quaternion.identity * Quaternion.Euler(-90, 0, -90));
                enemy.transform.SetParent(enemyUnits.transform);
                enemy = Instantiate(enemy, new Vector3(3f, -0.4f, 6f), Quaternion.identity * Quaternion.Euler(-90, 0, -90));
                enemy.transform.SetParent(enemyUnits.transform);
            }
        }


        public List<BaseTile> GetNeighbours(BaseTile tile)
        {
            List<BaseTile> neighbours = new List<BaseTile>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;
                    int checkX = tile.gridX + x;
                    int checkY = tile.gridY + y;
                    
                    
                    if (checkX != tile.gridX && checkY != tile.gridY)
                    {
                        continue;
                    }
                    
                    
                    if (checkX >= 0 && checkX < BoardSize && checkY >= 0 && checkY < BoardSize)
                    {
                        neighbours.Add(tileArray[checkX,checkY]);
                    }
                }
            }
            return neighbours;
        }
        

        public void OnTileSelected(Vector3 inputPosition)
        {
            Vector2Int coordinates = CalculateCoordinatesFromPosition(inputPosition);
            
            // IS IT PLAYERS TURN
            if (!controller.CanPerformMove())
                return;

            GameObject unit = GetUnitOnTile(coordinates);
            
            // IF SELECTED UNIT EXISTS
            if (selectedUnit)
            {
                if (unit != null && selectedUnit == unit)
                    // Deselect the unit (SELECTED UNIT IS BEING PRESSED -> DESELECT)
                    DeselectUnit();

                else if (unit != null && selectedUnit != unit && controller.IsTeamTurnActive())
                    // Select the unit (ANOTHER UNIT IS BEING PRESSED -> SELECT THE NEW ONE)
                    SelectUnit(coordinates);

                else if (selectedUnit.GetComponent<BaseTank>().CanMoveTo(coordinates))
                {
                    // Move to position (UNIT IS SELECTED AND CAN MOVE TO THE TILE PRESSED)
                    MoveSelectedUnit(coordinates);
                    DeselectUnit();
                    //ClearBoardMovement();
                }
            }
            // IF THERE IS NO SELECTED UNIT
            else
            {
                if (unit != null && controller.IsTeamTurnActive())
                    SelectUnit(coordinates);
            }
            Debug.Log("Nothing happened.");
        }

        public void OnUnitTargeting(Vector3 inputPosition)
        {
            Vector2Int endPosition = CalculateCoordinatesFromPosition(inputPosition);
            if (!selectedUnit)
                return;
            Vector2Int startPosition = selectedUnit.GetComponent<BaseTank>().position;
            if (endPosition.x >= 0 && endPosition.y >= 0 && endPosition.x <= BoardSize-1 && endPosition.y <= BoardSize-1)
                DrawPath(startPosition, endPosition);
        }

        public void ClearBoardMovement()
        {
            foreach (BaseTile tile in tileArray)
            {
                tile.SetDefaultMaterial();
            }
        }

        public void DrawMovableTiles(GameObject hoverUnit)
        {
            
        }
        
        public void DrawPath(Vector2Int start, Vector2Int end)
        {
            if (!tileArray[end.x, end.y].walkable)
                return;
            
            List<BaseTile> drawTiles = GetComponent<Pathfinding>().FindPath(tileArray[start.x, start.y], tileArray[end.x, end.y]);
            drawTiles = drawTiles.Where(tile => tile.GetComponent<BaseTile>().walkable).ToList();
            List<BaseTile> noDrawTiles = new List<BaseTile>();
            
            foreach (BaseTile tile in drawTiles)
            {
                if (tile.GetComponent<BaseTile>().walkable)
                    tile.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = _moveableTile;
            }
            
            foreach (BaseTile tile in tileArray)
            {
                if (!drawTiles.Contains(tile))
                {
                    noDrawTiles.Add(tile);
                }
            }

            foreach (BaseTile tile in noDrawTiles)
            {
                if (tile.walkable)
                    tile.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = _defaultTile;
            }
            
        }

        public void MoveSelectedUnit(Vector2Int endCoordinates)
        {
            Vector2Int pos = selectedUnit.GetComponent<BaseTank>().position;
            selectedUnit.GetComponent<BaseTank>().MoveTo(endCoordinates);
            Debug.Log(selectedUnit.name + " was moved");
        }
    
        private void SelectUnit(Vector2Int coordinates)
        {
            DeselectUnit();
            selectedUnit = GetUnitOnTile(coordinates);
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
                foreach (Transform child in playerUnits.transform)
                {
                    Vector2Int temp = child.GetComponent<BaseTank>().position;
                    if (temp.x == coordinates.x && temp.y == coordinates.y)
                    {
                        return child.gameObject;
                    }
                }
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
