using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
-------------------------------------------
This script handles the changes in tje games interface
-------------------------------------------
*/

public class InterfaceHandler : MonoBehaviour
{

    public int unSpawned;
    public Text text;

    void Start()
    {
        unSpawned = 3;
        GameObject.Find("ChooseSpawnText").GetComponent<Text>();
    }

    void Update()
    {
        //If every tank is spawned, delete the tip of choosing the spawns
        if (unSpawned <= 0)
        {
            text = GameObject.Find("ChooseSpawnText").GetComponent<Text>();
            text.enabled = false;
        }
    }
}
