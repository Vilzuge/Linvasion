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
   private int gridDimension = 8;
   private GameObject gameBoard;
   private List<GameObject> boardTileArray;
   private GameObject grassTile;
   private GameObject waterTile;


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
      boardTileArray = new List<GameObject>();
      int totalTiles = gridDimension * gridDimension;
      
      for (int i = 0; i <= totalTiles; i++)
      {
         float randomChance = Random.Range(0.0f, 1.0f);
         if (randomChance < 1f)
         {
            boardTileArray.Add(grassTile);
         }
         else
         {
            boardTileArray.Add(waterTile);
         }
      }
   }

   private void SpawnBoard()
   {
      // EMPTY THE OLD GAMEBOARD
      var tempList = gameBoard.transform.Cast<Transform>().ToList();
      foreach (var child in tempList)
      {
         GameObject.DestroyImmediate(child.gameObject);
      }
      
      // SPAWNING THE NEW GAMEBOARD
      for (int row = 0; row <= gridDimension - 1; row++)
      {
         for (int col = 0; col <= gridDimension - 1; col++)
         {
            int tileIndex = row * gridDimension + col;
            string tileType = boardTileArray[tileIndex].name;
            Debug.Log(tileIndex);
            Debug.Log(boardTileArray[tileIndex].name);
            
            GameObject newTile = Instantiate(boardTileArray[tileIndex], new Vector3(row, -0.5f, col), Quaternion.identity, gameBoard.transform);
            newTile.GetComponent<BaseTile>().row = row;
            newTile.GetComponent<BaseTile>().col = col;
            newTile.name = $"({row},{col}) {tileType}";
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
