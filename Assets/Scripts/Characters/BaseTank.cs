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
    //Material of the tank
    public Material defaultMaterial;
    public Material selectedMaterial;
    
    //Particlesystems
    public ParticleSystem explosion;
    
    //Gridmanager for managing the board
    private GridManager gridManager;
    
    //This is probably stupid
    private ToggleAim aimScript;
    private GameObject allEnemies;
    private Transform playerUnits;
    
    //Sound manager
    private SoundManagerScript soundManager;

    //Properties of base tank
    [Header("Tank Attributes")]
    public bool isSelected = false;
    public bool hasAction = true;
    public int rowPos;
    public int colPos;
    public int moveMinttu = 3;

    //Properties of raycast
    public float rayLength;
    public LayerMask layerMask;

    void Start()
    {
        defaultMaterial = Resources.Load<Material>("Materials/TankNormal");
        selectedMaterial = Resources.Load<Material>("Materials/TankSelected");
        
        gridManager = GameObject.Find("GameBoard").GetComponent<GridManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
        aimScript = GameObject.Find("Aim").GetComponent<ToggleAim>();
        allEnemies = GameObject.Find("EnemyUnits");
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

    void OnMouseUp() {
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
                    gridManager.ResetMovement();
                    isSelected = false;
                }
                else
                {
                    Debug.Log("Not walkable!");
                }
            }
        }

        if(!isSelected)
        {
            GetComponent<MeshRenderer>().material = defaultMaterial;
        }
    }

    public void handleShooting()
    {
        if (hasAction)
        {
            gridManager.DrawShootingGrid(rowPos, colPos);
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
                        Debug.Log("You shot the cell!!");

                        //handle the shooting enemies
                        damageCell(rowToShoot, colToShoot);
                        soundManager.PlaySound("playerShoot");
                        hasAction = false;
                        isSelected = false;
                        aimScript.isAiming = false;
                        gridManager.ResetShootingGrid();
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
                GameObject childTank = child.gameObject;
                if (childTank.GetComponent<LaserTank>() != null)
                {
                    child.GetComponent<LaserTank>().isSelected = false;
                    child.GetComponent<MeshRenderer>().material = child.GetComponent<LaserTank>().defaultMaterial;
                    gridManager.ResetMovement();
                }
                else if (childTank.GetComponent<StrongTank>() != null)
                {
                    child.GetComponent<StrongTank>().isSelected = false;
                    child.GetComponent<MeshRenderer>().material = child.GetComponent<StrongTank>().defaultMaterial;
                    gridManager.ResetMovement();
                }
                else
                {
                    Debug.Log("Something went wrong with selecting...");
                }
            }

            GetComponent<MeshRenderer>().material = selectedMaterial;
            //Drawing the board with possible movement tiles
            gridManager.DrawMovementGrid(rowPos, colPos, moveMinttu);

        }
        else
        {
            Debug.Log("This tank is not selected anymore.");
            isSelected = false;
            GetComponent<MeshRenderer>().material = defaultMaterial;
            gridManager.ResetMovement();
        }
    }


    //Check if the selected tile is walkable by the tank
    public bool isWalkable(int rowPosition, int colPosition)
    {
        foreach (Transform child in playerUnits)
        {
            if (child.position.x == rowPosition && child.position.z == colPosition)
            {
                return false;
            }
        }


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
        if(rowPosition == rowPos || colPosition == colPos)
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
        moveMinttu = 3;
        hasAction = true;
    }


    public void damageCell(int rowToShoot, int colToShoot)
    {
        foreach (Transform child in allEnemies.transform)
        {
            if (child.GetComponent<EnemyHandler>().enemyRow == rowToShoot && child.GetComponent<EnemyHandler>().enemyCol == colToShoot)
            {
                child.GetComponent<EnemyHandler>().enemyHealth -= 2;
            }
        }

        //playing particle effects!
        Instantiate(explosion, new Vector3(rowToShoot, 0.2f, colToShoot), Quaternion.identity);
    }
}