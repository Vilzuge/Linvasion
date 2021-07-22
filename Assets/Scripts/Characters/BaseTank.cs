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
    public Material defaultMaterial;
    public Material selectedMaterial;
    public ParticleSystem explosionEffect;
    
    private GridManager gridManager;
    private SoundManagerScript soundManager;
    
    public List<Vector2Int> availableMoves;
    public bool isPlayersUnit;
    
    protected int RowPos { get; set; }
    protected int ColPos { get; set; }



    public virtual void Start()
    { 
        gridManager = GameObject.Find("GameBoard").GetComponent<GridManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
        var position = transform.position;
        RowPos = (int)position.x;
        ColPos = (int)position.z;
        isPlayersUnit = true;
        //Debug.Log("From base: " + RowPos.ToString());
        //Debug.Log("From base: " + ColPos.ToString());
    }

    public bool CanMoveTo(Vector2Int coordinates)
    {
        // return availableMoves.Contains(coordinates);
        return true; // For testing only
    }

}