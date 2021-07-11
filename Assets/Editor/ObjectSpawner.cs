using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/*
-------------------------------------------
Copy paste from a video for reference
-------------------------------------------
*/
public class ObjectSpawner : EditorWindow
{
   private string objectName = "";
   private int objectID = 1;
   private GameObject objectToSpawn;
   private float objectScale;
   private float spawnRadius;

   [MenuItem("Tools/Object Spawner")]
   public static void ShowWindow()
   {
      GetWindow(typeof(ObjectSpawner));
   }

   private void OnGUI()
   {
      GUILayout.Label("Spawn New Object", EditorStyles.boldLabel);

      objectName = EditorGUILayout.TextField("Base Name", objectName);
      objectID = EditorGUILayout.IntField("Object ID", objectID);
      objectScale = EditorGUILayout.Slider("Object Scale", objectScale, 0.5f, 3f);
      spawnRadius = EditorGUILayout.FloatField("Spawn Radius", spawnRadius);
      objectToSpawn = EditorGUILayout.ObjectField("Prefab to Spawn", objectToSpawn, typeof(GameObject), false) as GameObject;

      if (GUILayout.Button("Spawn Object"))
      {
         SpawnObject();
      }
   }

   private void SpawnObject()
   {
      if (objectToSpawn == null)
      {
         Debug.LogError("Error: Please assign an object to be spawned.");
         return;
      }

      if (objectName == string.Empty)
      {
         Debug.LogError("Error: Please enter a base name for the object.");
         return;
      }

      Vector2 spawnCircle = Random.insideUnitCircle * spawnRadius;
      Vector3 spawnPos = new Vector3(spawnCircle.x, 0f, spawnCircle.y);

      GameObject newObject = Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
      newObject.name = objectName + objectID;
      newObject.transform.localScale = Vector3.one * objectScale;

      objectID++;

   }
}
