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
public class LaserTank : MonoBehaviour
{
    public Material defaultMaterial;
    public Material selectedMaterial;
    public DrawMovement movementScript;
    public DrawShooting shootingScript;
    public ToggleAim aimScript;
    public GameObject allEnemies;
    public Transform playerUnits;
    private SoundManagerScript soundManager;

    //Properties of the default tank
    public bool isSelected = false;
    public bool hasAction = true;
    public int rowPos;
    public int colPos;
    public int moveMinttu = 2;

    //Properties of raycast
    public float rayLength;
    public LayerMask layerMask;

    void Start()
    {
        defaultMaterial = Resources.Load<Material>("Materials/LaserTank");
        selectedMaterial = Resources.Load<Material>("Materials/TankSelected");
        movementScript = GameObject.Find("GameBoard").GetComponent<DrawMovement>();
        shootingScript = GameObject.Find("GameBoard").GetComponent<DrawShooting>();
        aimScript = GameObject.Find("Aim").GetComponent<ToggleAim>();
        allEnemies = GameObject.Find("EnemyUnits");
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
    }

    void Update()
    {
        playerUnits = GameObject.Find("PlayerUnits").transform;
        handleMovement();
        if (aimScript.isAiming && isSelected)
        {
            handleShooting();
        }
    }

    void OnMouseUp()
    {
        selectedCheck();
    }

    public void handleMovement()
    {
        //Handle movement if the tank is selected and a movable tile is selected, and we are not aiming currently
        if (isSelected && Input.GetMouseButtonDown(0) && moveMinttu > 0 && !aimScript.isAiming) //!EventSystem.current.IsPointerOverGameObject() add later if needed
        {
            //shootingScript.DrawShootingGrid(rowPos, colPos);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, rayLength, layerMask))
            {
                Debug.Log(hit.collider.name);
                Debug.Log(hit.transform.parent.gameObject.GetComponent<TileState>().RowIndex);
                Debug.Log(hit.transform.parent.gameObject.GetComponent<TileState>().ColIndex);
                int rowToWalk = hit.transform.parent.gameObject.GetComponent<TileState>().RowIndex;
                int colToWalk = hit.transform.parent.gameObject.GetComponent<TileState>().ColIndex;

                if (isWalkable(rowToWalk, colToWalk))
                {
                    Debug.Log("This cell is walkable!");
                    transform.position = new Vector3(rowToWalk, 0.1f, colToWalk);
                    moveMinttu = 0;
                    rowPos = rowToWalk;
                    colPos = colToWalk;
                    movementScript.ResetMovement();
                    isSelected = false;
                }
                else
                {
                    Debug.Log("Not walkable!");
                }
            }
        }

        if (!isSelected)
        {
            GetComponent<MeshRenderer>().material = defaultMaterial;
        }
    }

    public void handleShooting()
    {
        if (hasAction)
        {
            shootingScript.DrawShootingGrid(rowPos, colPos);
            if (isSelected && Input.GetMouseButtonDown(0)) //!EventSystem.current.IsPointerOverGameObject() add later if needed
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, rayLength, layerMask))
                {
                    Debug.Log(hit.collider.name);
                    Debug.Log(hit.transform.parent.gameObject.GetComponent<TileState>().RowIndex);
                    Debug.Log(hit.transform.parent.gameObject.GetComponent<TileState>().ColIndex);
                    int rowToShoot = hit.transform.parent.gameObject.GetComponent<TileState>().RowIndex;
                    int colToShoot = hit.transform.parent.gameObject.GetComponent<TileState>().ColIndex;

                    if (isShootable(rowToShoot, colToShoot))
                    {
                        Debug.Log("This cell is shootable!");
                        Debug.Log("You shot the laser!!");

                        //handle the shooting enemies
                        damagePlus(rowToShoot, colToShoot);
                        soundManager.PlaySound("laserShoot");
                        hasAction = false;
                        isSelected = false;
                        aimScript.isAiming = false;
                        shootingScript.ResetShootingGrid();
                    }
                    else
                    {
                        Debug.Log("Not shootable!");
                    }
                }
            }
        }
    }

    //Handle tank selection
    public void selectedCheck()
    {
        //Selecting/Deselecting the tank
        if (isSelected == false)
        {
            Debug.Log("This tank is now selected.");
            isSelected = true;

            //should deselect other tanks!
            foreach (Transform child in playerUnits)
            {
                if (!GameObject.ReferenceEquals(child.gameObject, this.gameObject))
                {
                    child.GetComponent<PlayerTank>().isSelected = false;
                    child.GetComponent<MeshRenderer>().material = defaultMaterial;
                    movementScript.ResetMovement();
                }
            }

            GetComponent<MeshRenderer>().material = selectedMaterial;
            //Drawing the board with possible movement tiles
            movementScript.DrawMovementGrid(rowPos, colPos, moveMinttu);

        }
        else
        {
            Debug.Log("This tank is not selected anymore.");
            isSelected = false;
            GetComponent<MeshRenderer>().material = defaultMaterial;
            movementScript.ResetMovement();
        }
    }


    //Check if the selected tile is walkable by the tank
    public bool isWalkable(int rowPosition, int colPosition)
    {
        if ((System.Math.Abs(rowPosition - rowPos) + System.Math.Abs(colPosition - colPos)) <= moveMinttu)
        {
            //is moveable
            return true;
        }
        else
        {
            //not moveable
            return false;
        }
    }

    //Is shootable

    public bool isShootable(int rowPosition, int colPosition)
    {
        //if the row or column index is the same than the place the tank is, the tile is shootable
        if (rowPosition == rowPos || colPosition == colPos)
        {
            //is shootable
            return true;
        }
        else
        {
            //not shootable
            return false;
        }
    }


    //Replenish the attributes of the tank when new turn starts
    public void replenishTank()
    {
        moveMinttu = 2;
        hasAction = true;
    }

    public void damagePlus(int rowToShoot, int colToShoot)
    {
        foreach (Transform child in allEnemies.transform)
        {
            if (child.GetComponent<EnemyHandler>().enemyRow == rowToShoot && child.GetComponent<EnemyHandler>().enemyCol == colToShoot)
            {
                child.GetComponent<EnemyHandler>().enemyHealth -= 1;
            }

            if (child.GetComponent<EnemyHandler>().enemyRow == rowToShoot-1 && child.GetComponent<EnemyHandler>().enemyCol == colToShoot)
            {
                child.GetComponent<EnemyHandler>().enemyHealth -= 1;
            }

            if (child.GetComponent<EnemyHandler>().enemyRow == rowToShoot+1 && child.GetComponent<EnemyHandler>().enemyCol == colToShoot)
            {
                child.GetComponent<EnemyHandler>().enemyHealth -= 1;
            }

            if (child.GetComponent<EnemyHandler>().enemyRow == rowToShoot && child.GetComponent<EnemyHandler>().enemyCol == colToShoot-1)
            {
                child.GetComponent<EnemyHandler>().enemyHealth -= 1;
            }

            if (child.GetComponent<EnemyHandler>().enemyRow == rowToShoot && child.GetComponent<EnemyHandler>().enemyCol == colToShoot+1)
            {
                child.GetComponent<EnemyHandler>().enemyHealth -= 1;
            }
        }
    }
}