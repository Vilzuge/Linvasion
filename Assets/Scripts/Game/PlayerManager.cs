using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
-------------------------------------------
This script takes input from the player and initiates actions 
-------------------------------------------
*/

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Debug.Log("Moii");
        }
    }
    
    private void MyMouseClick()
    {
        
    }
}
