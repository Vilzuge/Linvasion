using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Board;

/*
-------------------------------------------
Tool for generating a new random gameboard to the scene

Created initially to learn about editor scripts in Unity but was found quite useful later
-------------------------------------------
*/
public class BoardGenerator : EditorWindow
{
   private GameObject gameBoard;
   private TileBase[,] boardTileArray;
   private GameObject grassTile;
   private GameObject waterTile;
   private GameObject buildingTile;
   
   private GameObject playerUnit;
   private GameObject enemyUnit;
   
   private int BoardSize = 8;


   [MenuItem("Tools/Board Generator")]
   public static void ShowWindow()
   {
      GetWindow(typeof(BoardGenerator));
   }

   private void OnGUI()
   {
      GUILayout.Label("Generate new game board", EditorStyles.boldLabel);
      grassTile = EditorGUILayout.ObjectField("Grass Tile", grassTile, typeof(GameObject), false) as GameObject;
      waterTile = EditorGUILayout.ObjectField("Water Tile", waterTile, typeof(GameObject), false) as GameObject;
      buildingTile = EditorGUILayout.ObjectField("Building Tile", buildingTile, typeof(GameObject), false) as GameObject;

      if (GUILayout.Button("Spawn new board"))
      {
         FindBoard();
         SpawnBoard();
      }
      
      if (GUILayout.Button("Empty the board"))
      {
         EmptyBoard();
      }
      
      playerUnit = EditorGUILayout.ObjectField("Player Unit", playerUnit, typeof(GameObject), false) as GameObject;
      enemyUnit = EditorGUILayout.ObjectField("Enemy Unit", enemyUnit, typeof(GameObject), false) as GameObject;
      
      if (GUILayout.Button("Spawn units"))
      {
         //Do spawning stuff
      }
      
      if (GUILayout.Button("Clear units"))
      {
         //Do clearing stuff
      }
      
   }
   private void FindBoard()
   {
      gameBoard = GameObject.Find("GameBoard");
      
      if (gameBoard == null)
      {
         Debug.LogError("Error: Please assign an object to be spawned.");
         return;
      }
   }

   private void SpawnBoard()
   {
      boardTileArray = new TileBase[BoardSize, BoardSize];
      // EMPTY THE OLD GAMEBOARD
      var tempList = gameBoard.transform.Cast<Transform>().ToList();
      foreach (var child in tempList)
      {
         GameObject.DestroyImmediate(child.gameObject);
      }
      
      var tileTypeArray = new int[,]
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
            float randomChance = Random.Range(0.0f, 1.0f);
            if (tileTypeArray[row, col] == 1)
            {
               GameObject newTile = Instantiate(grassTile, new Vector3(row, -0.5f, col), Quaternion.identity);
               TileGrass component = newTile.AddComponent<TileGrass>();
               component.walkable = true;
               component.worldPosition = new Vector3(row, -0.5f, col);
               component.gridX = row;
               component.gridY = col;
               //tileArray[row, col] = newTile.GetComponent<TileGrass>();
               newTile.name = $"({row},{col}) {newTile.name}";
               newTile.transform.SetParent(gameBoard.transform);
            }
            else if (tileTypeArray[row, col] == 2)
            {
               GameObject newTile = Instantiate(waterTile, new Vector3(row, -0.60f, col), Quaternion.identity);
               TileWater component = newTile.AddComponent<TileWater>();
               component.walkable = false;
               component.worldPosition = new Vector3(row, -0.5f, col);
               component.gridX = row;
               component.gridY = col;
               //tileArray[row, col] = newTile.GetComponent<TileWater>();
               newTile.name = $"({row},{col}) {newTile.name}";
               newTile.transform.SetParent(gameBoard.transform);
            }
            else if (tileTypeArray[row, col] == 3)
            {
               GameObject newTile = Instantiate(buildingTile, new Vector3(row, -0.50f, col), Quaternion.identity);
               TileBuilding component = newTile.AddComponent<TileBuilding>();
               component.walkable = false;
               component.worldPosition = new Vector3(row, -0.5f, col);
               component.gridX = row;
               component.gridY = col;
               //tileArray[row, col] = newTile.GetComponent<TileBuilding>();
               newTile.name = $"({row},{col}) {newTile.name}";
               newTile.transform.SetParent(gameBoard.transform);
            }
         }
      }
   }

   private void EmptyBoard()
   {
      if (gameBoard == null)
      {
         return;
      }
      // EMPTY THE GAMEBOARD
      var tempList = gameBoard.transform.Cast<Transform>().ToList();
      foreach (var child in tempList)
      {
         GameObject.DestroyImmediate(child.gameObject);
      }
   }
}
