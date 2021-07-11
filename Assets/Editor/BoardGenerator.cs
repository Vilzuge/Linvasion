﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

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
      boardTileArray = new List<GameObject>();
      int totalTiles = gridDimension * gridDimension;
      
      for (int i = 0; i <= totalTiles; i++)
      {
         float randomChance = Random.Range(0.0f, 1.0f);
         if (randomChance < 0.75f)
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
      // EMPTY THE GAMEBOARD
      var tempList = gameBoard.transform.Cast<Transform>().ToList();
      foreach (var child in tempList)
      {
         GameObject.DestroyImmediate(child.gameObject);
      }
      
      // SPAWNING THE NEW BOARD
      for (int row = 0; row <= gridDimension - 1; row++)
      {
         for (int col = 0; col <= gridDimension - 1; col++)
         {
            int tileIndex = row * gridDimension + col;
            string tileType = boardTileArray[tileIndex].name;
            Debug.Log(tileIndex);
            Debug.Log(boardTileArray[tileIndex].name);
            GameObject newTile = Instantiate(boardTileArray[tileIndex], new Vector3(row, -0.5f, col), Quaternion.identity, gameBoard.transform);

            newTile.GetComponent<BaseTile>().rowIndex = row;
            newTile.GetComponent<BaseTile>().colIndex = col;
            //newTile.name = "juuh";
         }
      }
   }

   private void EmptyBoard()
   {
      // EMPTY THE GAMEBOARD
      var tempList = gameBoard.transform.Cast<Transform>().ToList();
      foreach (var child in tempList)
      {
         GameObject.DestroyImmediate(child.gameObject);
      }
   }
}
