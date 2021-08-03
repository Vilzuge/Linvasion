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
   private BaseTile[,] boardTileArray;
   private GameObject grassTile;
   private GameObject waterTile;
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

      if (GUILayout.Button("Spawn new board"))
      {
         FindBoard();
         GenerateBoard();
         SpawnBoard();
      }
      
      if (GUILayout.Button("Empty the board"))
      {
         EmptyBoard();
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
   
   private void GenerateBoard()
   {
      // GENERATING A NEW GAMEBOARD TO A LIST
      boardTileArray = new BaseTile[BoardSize, BoardSize];
   }

   private void SpawnBoard()
   {
      // EMPTY THE OLD GAMEBOARD
      var tempList = gameBoard.transform.Cast<Transform>().ToList();
      foreach (var child in tempList)
      {
         GameObject.DestroyImmediate(child.gameObject);
      }
      
      for (int row = 0; row <= BoardSize - 1; row++)
      {
         for (int col = 0; col <= BoardSize - 1; col++)
         {
            float randomChance = Random.Range(0.0f, 1.0f);
            if (randomChance < 0.8f)
            {
               GameObject newTile = Instantiate(grassTile, new Vector3(row, -0.5f, col), Quaternion.identity);
               TileGrass component = newTile.AddComponent<TileGrass>();
               component.walkable = true;
               component.worldPosition = new Vector3(row, -0.5f, col);
               component.gridX = row;
               component.gridY = col;
               boardTileArray[row, col] = newTile.GetComponent<TileGrass>();
               newTile.name = $"({row},{col}) {newTile.name}";
            }
            else
            {
               GameObject newTile = Instantiate(waterTile, new Vector3(row, -0.5f, col), Quaternion.identity);
               TileWater component = newTile.AddComponent<TileWater>();
               component.walkable = false;
               component.worldPosition = new Vector3(row, -0.5f, col);
               component.gridX = row;
               component.gridY = col;
               boardTileArray[row, col] = newTile.GetComponent<TileWater>();
               newTile.name = $"({row},{col}) {newTile.name}";
            }
         }
      }
      // ASSIGNING VALUES TO GRID MANAGER
      //gameBoard.GetComponent<GridManager>().BOARD_SIZE = gridDimension;
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
