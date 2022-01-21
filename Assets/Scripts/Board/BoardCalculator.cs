using System.Linq;
using System.Collections.Generic;
using Characters;
using UnityEngine;

namespace Board
{
    public class BoardCalculator : MonoBehaviour
    {
        private const int BoardSize = 8;
        private BaseTile[,] tileArray;
        [SerializeField] private GameObject playerUnits;
        [SerializeField] private GameObject enemyUnits;
        
        private void Start()
        {
            tileArray = FindTileArray();
            SetInitialOccupants();
        }

        // Getting reference to tile array
        private BaseTile[,] FindTileArray()
        {
            tileArray = new BaseTile[BoardSize,BoardSize];
            var tempList = transform.Cast<Transform>().ToList();
            foreach (var child in tempList)
            {
                var tileComponent = child.GetComponent<BaseTile>();
                tileArray[tileComponent.gridX, tileComponent.gridY] = child.GetComponent<BaseTile>();
            }
            return tileArray;
        }
        
        
        // Returns neighbouring tiles of the parameter tile (N,E,S,W)
        public List<BaseTile> GetNeighbours(BaseTile baseTile)
        {
            var neighbours = new List<BaseTile>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;
                    int checkX = baseTile.gridX + x;
                    int checkY = baseTile.gridY + y;
                    
                    if (checkX != baseTile.gridX && checkY != baseTile.gridY)
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
        
        // Getting a unit that occupies a tile
        public GameObject GetUnitOnTile(Vector2Int coordinates)
        {
            if (!CheckIfCoordinatesAreOnBoard(coordinates))
            {
                return null;
            }
            
            foreach (Transform child in playerUnits.transform)
            {
                var temp = child.GetComponent<UnitMovement>().position;
                if (temp.x == coordinates.x && temp.y == coordinates.y)
                {
                    return child.gameObject;
                }
            }
                
            foreach (Transform child in enemyUnits.transform)
            {
                var temp = child.GetComponent<UnitMovement>().position;
                if (temp.x == coordinates.x && temp.y == coordinates.y)
                {
                    return child.gameObject;
                }
            }
            return null;
        }
        
        // Check if the given coordinates are on the board
        public static bool CheckIfCoordinatesAreOnBoard(Vector2Int coordinates)
        {
            return coordinates.x >= 0 && coordinates.y >= 0 && coordinates.x < BoardSize && coordinates.y < BoardSize;
        }
        
        // Calculating 2D coordinates from a 3D vector
        public Vector2Int CalculateCoordinatesFromPosition(Vector3 inputPosition)
        {
            if (inputPosition == new Vector3(-1, -1, -1))
                return new Vector2Int(-1, -1);
            int x = Mathf.FloorToInt(transform.InverseTransformPoint(inputPosition).x + 0.5f);
            int y = Mathf.FloorToInt(transform.InverseTransformPoint(inputPosition).z + 0.5f);
            return new Vector2Int(x, y);
        }
        
        
        public List<BaseTile> CalculateMovableTiles(GameObject unit)
        {
            Vector2Int unitPos = unit.GetComponent<UnitMovement>().position;
            int unitMovement = unit.GetComponent<UnitMovement>().movementValue;
            
            if (tileArray == null) return null;
            var moveTiles = new List<BaseTile> {tileArray[unitPos.x, unitPos.y]};
            for (var i = 0; i < unitMovement; i++)
            {
                foreach (BaseTile tile in moveTiles.ToList())
                {
                    var neighbours = GetNeighbours(tile);
                    foreach (BaseTile neighbour in neighbours)
                    {
                        if (moveTiles.Contains(neighbour) || !neighbour.IsWalkable())
                        {
                            continue;
                        }
                        if (neighbour is TileGrass)
                        {
                            moveTiles.Add(neighbour);
                        }
                    }
                }
            }
            return moveTiles;
        }
        
        public List<BaseTile> CalculateShootableTiles(GameObject unit)
        {
            Vector2Int unitPos = unit.GetComponent<UnitMovement>().position;

            var shootTiles = new List<BaseTile>();
            foreach (BaseTile tile in tileArray)
            {
                if ((tile.gridX == unitPos.x) ^ (tile.gridY == unitPos.y)) // logical exclusive OR
                    shootTiles.Add(tile);
            }
            return shootTiles;
        }
        
        private void SetInitialOccupants()
        {
            foreach (BaseTile tile in tileArray)
            {
                var row = (int) tile.worldPosition.x;
                var col = (int) tile.worldPosition.z;
                if (GetUnitOnTile(new Vector2Int(row, col)) != null)
                {
                    tileArray[row, col].SetUnWalkable();
                }
            }
        }

        public BaseTile GetTile(Vector2Int coordinates)
        {
            return tileArray[coordinates.x, coordinates.y];
        }

        public BaseTile[,] GetTileArray()
        {
            return tileArray;
        }
        
        public int GetBoardSize()
        {
            return BoardSize;
        }

    }
}
