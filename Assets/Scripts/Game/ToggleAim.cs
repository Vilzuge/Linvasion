using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAim : MonoBehaviour
{
    public bool isAiming = false;
    public DrawShooting shootingScript;

    // Start is called before the first frame update
    void Start()
    {
        shootingScript = GameObject.Find("GameBoard").GetComponent<DrawShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleAiming()
    {
        if (!isAiming)
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
            shootingScript.ResetShootingGrid();
        }
    }
}
