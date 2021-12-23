﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Board;
using Characters;

/*
-------------------------------------------
Tool for generating a new random gameboard to the scene

Created initially to learn about editor scripts in Unity but was found quite useful later
-------------------------------------------
*/
public class BoardGenerator : EditorWindow
{
   private GameObject gameBoard;

   private GameObject grassTile;
   private GameObject waterTile;
   private GameObject buildingTile;
   
   private GameObject playerUnit;
   private GameObject enemyUnit;
   
   private GameObject playerUnitList;
   private GameObject enemyUnitList;
   
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
         FindUnitParents();
         SpawnUnits();
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

   private void FindUnitParents()
   {
      playerUnitList = GameObject.Find("PlayerUnits");
      enemyUnitList = GameObject.Find("EnemyUnits");
      if (playerUnitList == null || enemyUnitList == null)
      {
         Debug.Log("parents not found...");
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
               TileGrass component = newTile.GetComponent<TileGrass>();
               component.worldPosition = new Vector3(row, -0.5f, col);
               component.gridX = row;
               component.gridY = col;
               newTile.name = $"({row},{col}) {newTile.name}";
               newTile.transform.SetParent(gameBoard.transform);
            }
            else if (tileTypeArray[row, col] == 2)
            {
               GameObject newTile = Instantiate(waterTile, new Vector3(row, -0.60f, col), Quaternion.identity);
               TileWater component = newTile.GetComponent<TileWater>();
               component.worldPosition = new Vector3(row, -0.5f, col);
               component.gridX = row;
               component.gridY = col;
               newTile.name = $"({row},{col}) {newTile.name}";
               newTile.transform.SetParent(gameBoard.transform);
            }
            else if (tileTypeArray[row, col] == 3)
            {
               GameObject newTile = Instantiate(buildingTile, new Vector3(row, -0.50f, col), Quaternion.identity);
               TileBuilding component = newTile.GetComponent<TileBuilding>();
               component.worldPosition = new Vector3(row, -0.5f, col);
               component.gridX = row;
               component.gridY = col;
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
   
   
   private void SpawnUnits()
   {
      if (!playerUnit || !enemyUnit) return;
       
      //friendlies
      SpawnUnit(playerUnit, new Vector3(1f, -0.4f, 3f), false);
      SpawnUnit(playerUnit, new Vector3(3f, -0.4f, 2f), false);
      SpawnUnit(playerUnit, new Vector3(6f, -0.4f, 2f), false);
      
      //enemies
      SpawnUnit(enemyUnit, new Vector3(4f, 0f, 7f), true);
      SpawnUnit(enemyUnit, new Vector3(6f, 0f, 7f), true);
      SpawnUnit(enemyUnit, new Vector3(3f, 0f, 6f), true);
        
   }

    
   private void SpawnUnit(GameObject unit, Vector3 position, bool isEnemy)
   {
      unit = Instantiate(unit, position, Quaternion.identity);
        
      if (isEnemy)
      {
         unit.gameObject.transform.rotation = Quaternion.identity * Quaternion.Euler(-180, 0, 0); // purkkaa, modeli vinossa
         unit.transform.SetParent(enemyUnitList.transform);
      }
      else
      {
         unit.transform.SetParent(playerUnitList.transform);
      }
   }
   
}
