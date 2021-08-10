using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Board
{
    public class BoardGenerator : MonoBehaviour
    {
        
        public BaseTile[,] tileArray;
        public GameObject grassTile;
        public GameObject waterTile;
        public const int BoardSize = 8;
        
        void Awake()
        {
            tileArray = new BaseTile[BoardSize, BoardSize];
        }
        
        public BaseTile[,] GenerateBoard()
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
            return tileArray;
        }
    }
}