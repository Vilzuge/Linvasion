using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoardGenerator : EditorWindow
{
   private int gridDimension = 8;
   private GameObject gameBoard;
   private GameObject defaultTile;


   [MenuItem("Tools/Board Generator")]
   public static void ShowWindow()
   {
      GetWindow(typeof(BoardGenerator));
   }

   private void OnGUI()
   {
      GUILayout.Label("Generate new game board", EditorStyles.boldLabel);
      defaultTile = EditorGUILayout.ObjectField("Single tile", defaultTile, typeof(GameObject), false) as GameObject;

      if (GUILayout.Button("Spawn new board"))
      {
         FindBoard();
         GenerateBoard();
      }
   }

   private void GenerateBoard()
   {
      foreach (Transform child in gameBoard.transform)
      {
         GameObject.DestroyImmediate(child.gameObject);
      }
      
      for (int row = 0; row <= gridDimension - 1; row++)
      {
         for (int col = 0; col <= gridDimension - 1; col++)
         {
            GameObject newTile = Instantiate(defaultTile, new Vector3(row, -0.5f, col), Quaternion.identity, gameBoard.transform);
         }
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
}
