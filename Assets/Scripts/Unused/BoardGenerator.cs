using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
-------------------------------------------
This script generates a new gameboard
-------------------------------------------
*/

namespace Board
{
    public class BoardGenerator : MonoBehaviour
    {
        private const int BoardSize = 8;
        private TileBase[,] tileArray;
        [SerializeField] private GameObject grassTile;
        [SerializeField] private GameObject waterTile;
        [SerializeField] private GameObject buildingTile;

        private void Awake()
        {
            tileArray = new TileBase[BoardSize, BoardSize];
        }
        
        // Randomize a board
        public TileBase[,] GenerateBoard()
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
                    if (randomChance < 0.85f)
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
                    else if (randomChance < 0.95)
                    {
                        GameObject newTile = Instantiate(waterTile, new Vector3(row, -0.60f, col), Quaternion.identity, transform);
                        TileWater component = newTile.AddComponent<TileWater>();
                        component.walkable = false;
                        component.worldPosition = new Vector3(row, -0.5f, col);
                        component.gridX = row;
                        component.gridY = col;
                        tileArray[row, col] = newTile.GetComponent<TileWater>();
                        newTile.name = $"({row},{col}) {newTile.name}";
                    }
                    else
                    {
                        GameObject newTile = Instantiate(buildingTile, new Vector3(row, -0.50f, col), Quaternion.identity, transform);
                        TileBuilding component = newTile.AddComponent<TileBuilding>();
                        component.walkable = false;
                        component.worldPosition = new Vector3(row, -0.5f, col);
                        component.gridX = row;
                        component.gridY = col;
                        tileArray[row, col] = newTile.GetComponent<TileBuilding>();
                        newTile.name = $"({row},{col}) {newTile.name}";
                    }
                }
            }
            return tileArray;
        }
        
        // Potato version for creating a custom grid without randomization 
        public TileBase[,] GeneratePredetermined()
        {
            var tempList = transform.Cast<Transform>().ToList();
            foreach (var child in tempList)
            {
                GameObject.DestroyImmediate(child.gameObject);
            }

            int [,] tileTypeArray = new int[,]
            {
                { 1, 1, 1, 1, 2, 1, 1, 1},
                { 1, 1, 1, 1, 2, 1, 1, 1},
                { 1, 1, 1, 1, 3, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 1},
                { 2, 2, 1, 1, 1, 1, 1, 1},
                { 2, 2, 3, 3, 1, 1, 1, 1},
                { 1, 1, 1, 3, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 1},
            };

            for (int row = 0; row <= BoardSize - 1; row++)
            {
                for (int col = 0; col <= BoardSize - 1; col++)
                {
                    if (tileTypeArray[row, col] == 1)
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
                    else if (tileTypeArray[row, col] == 2)
                    {
                        GameObject newTile = Instantiate(waterTile, new Vector3(row, -0.60f, col), Quaternion.identity, transform);
                        TileWater component = newTile.AddComponent<TileWater>();
                        component.walkable = false;
                        component.worldPosition = new Vector3(row, -0.5f, col);
                        component.gridX = row;
                        component.gridY = col;
                        tileArray[row, col] = newTile.GetComponent<TileWater>();
                        newTile.name = $"({row},{col}) {newTile.name}";
                    }
                    
                    else if (tileTypeArray[row, col] == 3)
                    {
                        GameObject newTile = Instantiate(buildingTile, new Vector3(row, -0.50f, col), Quaternion.identity, transform);
                        TileBuilding component = newTile.AddComponent<TileBuilding>();
                        component.walkable = false;
                        component.worldPosition = new Vector3(row, -0.5f, col);
                        component.gridX = row;
                        component.gridY = col;
                        tileArray[row, col] = newTile.GetComponent<TileBuilding>();
                        newTile.name = $"({row},{col}) {newTile.name}";
                    }
                }
            }
            return tileArray;
        }
    }
}
