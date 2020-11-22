using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
-------------------------------------------
Handles the UI parts of spawning the players tanks
-------------------------------------------
*/
public class TankSpawner : MonoBehaviour
{
    public DrawMovement movementScript;
    [Header("default/artillery")]
    public string tankType;

    void Start()
    {
        movementScript = GameObject.Find("GameBoard").GetComponent<DrawMovement>();
    }
    void Update()
    {
        
    }
    public void ChooseSpawn()
    {
        //Show's the possible tiles for spawning and changes them to spawnable
        movementScript.DrawSpawnGrid();
        //Makes the UI button unclickable after the tank has been deployed
        GetComponent<Button>().interactable = false;
    }

    public void ChooseLaserSpawn()
    {
        //Show's the possible tiles for spawning and changes them to spawnable
        movementScript.DrawLaserSpawnGrid();
        //Makes the UI button unclickable after the tank has been deployed
        GetComponent<Button>().interactable = false;
    }
}