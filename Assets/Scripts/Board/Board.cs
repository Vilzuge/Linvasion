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
        public Vector2Int mousePosition;
    
        public GameObject grassTile;
        public GameObject waterTile;
        
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
            mousePosition = new Vector2Int();
            Debug.Log("Materials loaded");
        
            //GenerateBoard();
            tileArray = GetComponent<BoardGenerator>().GenerateBoard();
            Debug.Log("Board generated");
        
            InitializeGame();
            Debug.Log("Game initialized");
        
        }

        void Update()
        {
            GameObject hoverUnit = GetUnitOnTile(mousePosition);
            if (hoverUnit)
                DrawMovableTiles(hoverUnit);
            else
            {
                ClearBoardMovement();
            }
        }
        
        // Creating the game controller and setting game state
        private void InitializeGame()
        {
            controller = Instantiate(controller);
            controller.SetGameState(GameState.Play);
            GetComponent<SpawnDebug>().SpawnDebugUnits();
        }



        // Get neighbour tiles of a tile (top bottom right left, no diagonals)
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
        

        // Handles unit selection and movement by tracking clicks on the gameboard's colliders
        public void OnTileSelected(Vector3 inputPosition)
        {
            Vector2Int coordinates = CalculateCoordinatesFromPosition(inputPosition);
            
            // IS IT PLAYERS TURN
            if (!controller.CanPerformMove())
                return;

            GameObject unitObject = GetUnitOnTile(coordinates);

            // IF SELECTED UNIT EXISTS
            if (selectedUnit)
            {

                if (unitObject != null && selectedUnit == unitObject)
                    // Deselect the unit (SELECTED UNIT IS BEING PRESSED -> DESELECT)
                    DeselectUnit();

                else if (unitObject != null && selectedUnit != unitObject && controller.IsTeamTurnActive())
                    // Select the unit (ANOTHER UNIT IS BEING PRESSED -> SELECT THE NEW ONE)
                    SelectUnit(coordinates);

                else if (selectedUnit.GetComponent<BaseUnit>().CanMoveTo(coordinates) && selectedUnit.GetComponent<BaseUnit>().state == TankState.Selected)
                {
                    // Move to position (UNIT IS SELECTED AND CAN MOVE TO THE TILE PRESSED)
                    MoveSelectedUnit(coordinates);
                    DeselectUnit();
                    ClearBoardMovement();
                }
                else if (selectedUnit.GetComponent<BaseUnit>().state == TankState.Aiming)
                {
                    // Shoot to position (UNIT IS AIMING AND TRIES TO SHOOT AT COORDS)
                    selectedUnit.GetComponent<BaseUnit>().TryToShoot(coordinates);
                }
            }
            // IF THERE IS NO SELECTED UNIT
            else
            {
                if (unitObject != null && controller.IsTeamTurnActive())
                {
                    SelectUnit(coordinates);
                }
            }
            Debug.Log("Nothing happened.");
        }

        public void OnUnitTargeting(Vector3 inputPosition)
        {
            Vector2Int endPosition = CalculateCoordinatesFromPosition(inputPosition);
            mousePosition = endPosition;
            if (!selectedUnit || !CheckIfCoordinatesAreOnBoard(mousePosition))
                return;
            Vector2Int startPosition = selectedUnit.GetComponent<BaseUnit>().position;
            if (mousePosition.x >= 0 && mousePosition.y >= 0 && mousePosition.x <= BoardSize-1 && mousePosition.y <= BoardSize-1)
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
            int moves = hoverUnit.transform.GetComponent<BaseUnit>().movementValue;
            Vector2Int myPos = hoverUnit.transform.GetComponent<BaseUnit>().position;
            List<BaseTile> moveTiles = new List<BaseTile>();
            moveTiles.Add(tileArray[myPos.x, myPos.y]);

            for (int i = 0; i < moves; i++)
            {
                foreach (BaseTile tile in moveTiles.ToList())
                {
                    List<BaseTile> neighbours = GetNeighbours(tile);
                    foreach (BaseTile loopTile in neighbours)
                    {
                        if (!moveTiles.Contains(loopTile))
                            if (loopTile is TileGrass)
                                moveTiles.Add(loopTile);
                    }
                }
            }
            
            foreach (BaseTile tile in moveTiles)
            {
                tile.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = _spawnableTile;
            }
            
        }
        
        public void DrawPath(Vector2Int start, Vector2Int end)
        {
            if (!tileArray[end.x, end.y].walkable)
                return;
            
            List<BaseTile> drawTiles = GetComponent<Pathfinding>().FindPath(tileArray[start.x, start.y], tileArray[end.x, end.y]);
            if (drawTiles.Any())
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

        public void ApplyDamage(Vector2Int coordsShotAt, int damageValue)
        {
            BaseTile damagedTile = tileArray[coordsShotAt.x, coordsShotAt.y];
            GameObject damagedUnit = GetUnitOnTile(coordsShotAt);
            damagedUnit.GetComponent<BaseUnit>().health -= damageValue;
        }

        public void MoveSelectedUnit(Vector2Int endCoordinates)
        {
            Vector2Int pos = selectedUnit.GetComponent<BaseUnit>().position;
            selectedUnit.GetComponent<BaseUnit>().MoveTo(endCoordinates);
            Debug.Log(selectedUnit.name + " was moved");
        }
    
        private void SelectUnit(Vector2Int coordinates)
        {
            DeselectUnit();
            selectedUnit = GetUnitOnTile(coordinates);
            selectedUnit.GetComponent<BaseUnit>().SetSelected();
            Debug.Log(selectedUnit.name + " was selected");
            // TODO: Drawing movable tiles
        }
        private void DeselectUnit()
        {
            if (!selectedUnit) return;
            Debug.Log(selectedUnit.name + " will be deselected");
            selectedUnit.GetComponent<BaseUnit>().SetDeselected();
            selectedUnit = null;
        }

        public GameObject GetUnitOnTile(Vector2Int coordinates)
        {
            if (CheckIfCoordinatesAreOnBoard(coordinates))
            {
                foreach (Transform child in playerUnits.transform)
                {
                    Vector2Int temp = child.GetComponent<BaseUnit>().position;
                    if (temp.x == coordinates.x && temp.y == coordinates.y)
                    {
                        return child.gameObject;
                    }
                }
                
                foreach (Transform child in enemyUnits.transform)
                {
                    Vector2Int temp = child.GetComponent<BaseUnit>().position;
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
