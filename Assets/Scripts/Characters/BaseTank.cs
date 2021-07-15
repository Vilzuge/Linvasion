using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;

/*
-------------------------------------------
This script handles default tank of the player
-------------------------------------------
*/
public class BaseTank : MonoBehaviour
{
    // MATERIALS NORMAL/SELECTED
    public Material defaultMaterial;
    public Material selectedMaterial;
    
    // PARTICLE SYSTEMS
    public ParticleSystem explosionEffect;
    
    // Managers
    private GridManager gridManager;
    private SoundManagerScript soundManager;

    // PROPERTIES
    [field: Header("Tank Attributes")]
    public int RowPos { get; set; }
    public int ColPos { get; set; }

    //private int statMovement = 3;
    //private bool hasAction = true;

    
    
    void Start()
    { 
        gridManager = GameObject.Find("GameBoard").GetComponent<GridManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
    }

    void Update()
    {

    }
    
    // HANDLING MOVEMENT
    public void moveTo(int row, int col)
    {
        // If the cell is valid, move there
        transform.position = new Vector3(row, 0.1f, col);
    }

    // HANDLING SHOOTING
    public void handleShooting()
    {
        
    }

    // HANDLE SELECTION
    public void selectedCheck()
    {

    }


    //Check if the selected tile is walkable by the tank
    public bool isWalkable(int rowPosition, int colPosition)
    {
        return true;
    }

    //Is shootable
    public bool isShootable(int rowPosition, int colPosition)
    {
        return true;
    }


    //Replenish the attributes of the tank when new turn starts
    public void replenishTank()
    {

    }


    public void damageCell(int rowToShoot, int colToShoot)
    {

    }
}