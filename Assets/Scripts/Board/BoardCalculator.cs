using System.Linq;
using System.Collections.Generic;
using Characters;
using UnityEngine;

namespace Board
{
    public class BoardCalculator : MonoBehaviour
    {
        private const int BoardSize = 8;
        private TileBase[,] tileArray;
        [SerializeField] private GameObject playerUnits;
        [SerializeField] private GameObject enemyUnits;
        
        private void Start()
        {
            tileArray = FindTileArray();
            SetInitialOccupants();
        }

        // Getting reference to tile array
        private TileBase[,] FindTileArray()
        {
            tileArray = new TileBase[BoardSize,BoardSize];
            var tempList = transform.Cast<Transform>().ToList();
            foreach (var child in tempList)
            {
                var tileComponent = child.GetComponent<TileBase>();
                tileArray[tileComponent.gridX, tileComponent.gridY] = child.GetComponent<TileBase>();
            }
            return tileArray;
        }
        
        
        // Returns neighbouring tiles of the parameter tile (N,E,S,W)
        public List<TileBase> GetNeighbours(TileBase tile)
        {
            var neighbours = new List<TileBase>();
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
        
        // Getting a unit that occupies a tile
        public GameObject GetUnitOnTile(Vector2Int coordinates)
        {
            if (!CheckIfCoordinatesAreOnBoard(coordinates)) return null;
            
            foreach (Transform child in playerUnits.transform)
            {
                var temp = child.GetComponent<TankBase>().position;
                if (temp.x == coordinates.x && temp.y == coordinates.y)
                {
                    return child.gameObject;
                }
            }
                
            foreach (Transform child in enemyUnits.transform)
            {
                var temp = child.GetComponent<EnemyBase>().position;
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
        
        
        public List<TileBase> CalculateMovableTiles(Vector2Int unitPos, int unitMovement)
        {
            if (tileArray == null) return null;
            var moveTiles = new List<TileBase> {tileArray[unitPos.x, unitPos.y]};
            for (var i = 0; i < unitMovement; i++)
            {
                foreach (TileBase tile in moveTiles.ToList())
                {
                    var neighbours = GetNeighbours(tile);
                    foreach (TileBase loopTile in neighbours)
                    {
                        if (moveTiles.Contains(loopTile) || !loopTile.IsWalkable()) continue;
                        if (loopTile is TileGrass)
                            moveTiles.Add(loopTile);
                    }
                }
            }
            return moveTiles;
        }

        private void SetInitialOccupants()
        {
            foreach (TileBase tile in tileArray)
            {
                var row = (int) tile.worldPosition.x;
                var col = (int) tile.worldPosition.z;
                if (GetUnitOnTile(new Vector2Int(row, col)) != null)
                {
                    tileArray[row, col].SetUnWalkable();
                }
            }
        }

        public TileBase[,] GetTileArray()
        {
            return tileArray;
        }
        
        public int GetBoardSize()
        {
            return BoardSize;
        }

    }
}
