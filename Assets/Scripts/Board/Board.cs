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
        public const int BoardSize = 8;
        [SerializeField] private GameController controller;
        private TileBase[,] tileArray;
        public List<TileBase> path;
        public Vector2Int mousePosition;
        
        private GameObject selectedUnit;
        public GameObject playerUnits;
        public GameObject enemyUnits;

        void Awake()
        {
            tileArray = new TileBase[BoardSize, BoardSize];
        }
        
        void Start()
        {
            mousePosition = new Vector2Int();

            tileArray = GetComponent<BoardGenerator>().GenerateBoard();
            Debug.Log("Board generated");
        
            InitializeGame();
            Debug.Log("Game initialized");
        
        }
        
        void Update()
        {
            GameObject hoverUnit = GetUnitOnTile(mousePosition);
            if (hoverUnit && hoverUnit.GetComponent<BaseUnit>())
                DrawMovableTiles(hoverUnit);
            else
            {
                ClearBoardMovement();
            }
            TryDrawPath();
            
            
        }
        
        // Initialize the game, set a controller and spawn (debug) units
        private void InitializeGame()
        {
            controller = Instantiate(controller);
            controller.SetGameState(GameState.Play);
            GetComponent<SpawnDebug>().SpawnDebugUnits();
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
                // SELECTED UNIT IS BEING PRESSED -> DESELECT
                if (unitObject != null && selectedUnit == unitObject)
                    DeselectUnit();

                // ANOTHER UNIT IS BEING PRESSED -> SELECT THE NEW ONE
                else if (unitObject != null && selectedUnit != unitObject && controller.IsTeamTurnActive())
                    SelectUnit(coordinates);
                
                // UNIT IS SELECTED AND CAN MOVE TO THE TILE PRESSED
                else if (selectedUnit.GetComponent<BaseUnit>().CanMoveTo(coordinates) && selectedUnit.GetComponent<BaseUnit>().state == TankState.Selected)
                {
                    MoveSelectedUnit(coordinates);
                    DeselectUnit();
                    ClearBoardMovement();
                }
                
                // UNIT IS AIMING AND TRIES TO SHOOT AT COORDS
                else if (selectedUnit.GetComponent<BaseUnit>().state == TankState.Aiming)
                {
                    selectedUnit.GetComponent<BaseUnit>().TryToShoot(coordinates);
                }
            }
            // IF THERE IS NO SELECTED UNIT
            else
            {
                // NO UNITS SELECTED -> SELECT THE NEW ONE
                if (unitObject != null && controller.IsTeamTurnActive() && unitObject.GetComponent<BaseUnit>())
                {
                    SelectUnit(coordinates);
                }
            }
        }
        
        // Tracking the current mouse position
        public void TrackMousePosition(Vector3 inputPosition)
        {
            Vector2Int endPosition = CalculateCoordinatesFromPosition(inputPosition);
            mousePosition = endPosition;
        }
        
        // Draw path if unit is selected
        public void TryDrawPath()
        {
            if (!selectedUnit || !CheckIfCoordinatesAreOnBoard(mousePosition))
                return;
            Vector2Int startPosition = selectedUnit.GetComponent<BaseUnit>().position;
            if (mousePosition.x >= 0 && mousePosition.y >= 0 && mousePosition.x <= BoardSize-1 && mousePosition.y <= BoardSize-1)
                DrawPath(startPosition, mousePosition);
        }
        
        // Get neighbour tiles of a tile (top bottom right left, no diagonals)
        public List<TileBase> GetNeighbours(TileBase tile)
        {
            List<TileBase> neighbours = new List<TileBase>();

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

        public void DrawMovableTiles(GameObject hoverUnit)
        {
            int moves = hoverUnit.transform.GetComponent<BaseUnit>().movementValue;
            Vector2Int myPos = hoverUnit.transform.GetComponent<BaseUnit>().position;
            List<TileBase> moveTiles = new List<TileBase>();
            moveTiles.Add(tileArray[myPos.x, myPos.y]);

            for (int i = 0; i < moves; i++)
            {
                foreach (TileBase tile in moveTiles.ToList())
                {
                    List<TileBase> neighbours = GetNeighbours(tile);
                    foreach (TileBase loopTile in neighbours)
                    {
                        if (!moveTiles.Contains(loopTile))
                            if (loopTile is TileGrass)
                                moveTiles.Add(loopTile);
                    }
                }
            }
            
            foreach (TileBase tile in moveTiles)
            {
                tile.SetMovableMaterial();
            }
            
        }
        
        public void DrawPath(Vector2Int start, Vector2Int end)
        {
            if (!tileArray[end.x, end.y].walkable)
                return;
            
            List<TileBase> drawTiles = GetComponent<Pathfinding>().FindPath(tileArray[start.x, start.y], tileArray[end.x, end.y]);
            if (drawTiles.Any())
                drawTiles = drawTiles.Where(tile => tile.GetComponent<TileBase>().walkable).ToList();
            List<TileBase> noDrawTiles = new List<TileBase>();
            
            foreach (TileBase tile in drawTiles)
            {
                if (tile.GetComponent<TileBase>().walkable)
                    tile.SetPathMaterial();
            }
            
            foreach (TileBase tile in tileArray)
            {
                if (!drawTiles.Contains(tile))
                {
                    noDrawTiles.Add(tile);
                }
            }

            foreach (TileBase tile in noDrawTiles)
            {
                if (tile.walkable)
                    tile.SetDefaultMaterial();
            }
            
        }
        
        public void ClearBoardMovement()
        {
            foreach (TileBase tile in tileArray)
            {
                tile.SetDefaultMaterial();
            }
        }

        public void ApplyDamage(Vector2Int coordsShotAt, int damageValue)
        {
            TileBase damaged = tileArray[coordsShotAt.x, coordsShotAt.y];
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
                    Vector2Int temp = child.GetComponent<BaseEnemy>().position;
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
