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
        
        void Start()
        {
            tileArray = FindTileArray();
        }

        // Getting reference to tile array
        public TileBase[,] FindTileArray()
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
            if (CheckIfCoordinatesAreOnBoard(coordinates))
            {
                foreach (Transform child in playerUnits.transform)
                {
                    Vector2Int temp = child.GetComponent<TankBase>().position; //GetComponent<TankBase>().position;
                    if (temp.x == coordinates.x && temp.y == coordinates.y)
                    {
                        return child.gameObject;
                    }
                }
                
                foreach (Transform child in enemyUnits.transform)
                {
                    Vector2Int temp = child.GetComponent<EnemyBase>().position;
                    if (temp.x == coordinates.x && temp.y == coordinates.y)
                    {
                        return child.gameObject;
                    }
                }
            }
            return null;
        }
        
        // Check if the given coordinates are on the board
        public bool CheckIfCoordinatesAreOnBoard(Vector2Int coordinates)
        {
            if (coordinates.x < 0 || coordinates.y < 0 || coordinates.x >= BoardSize || coordinates.y >= BoardSize)
            {
                return false;
            }
            return true;
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
            List<TileBase> moveTiles = new List<TileBase>();
            moveTiles.Add(tileArray[unitPos.x, unitPos.y]);
            for (int i = 0; i < unitMovement; i++)
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
            return moveTiles;
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
