using System.Collections.Generic;
using Board;
using SFX;
using UnityEngine;

/*
-------------------------------------------
This script handles default tank of the player
-------------------------------------------
*/
namespace Characters
{
    public class BaseTank : MonoBehaviour
    {
        public Material _defaultMaterial;
        public Material _selectedMaterial;
        public Canvas myCanvas;
        public GameObject aimButton;

        private GridManager gridManager;
        private SoundManagerScript soundManager;
    
        public List<Vector2Int> availableMoves;
        public bool isPlayersUnit;

        public Vector2Int position;
    
    
        protected virtual void Start()
        { 
            gridManager = GameObject.Find("GameBoard").GetComponent<GridManager>();
            soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
            myCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        
            var positionPhysically = transform.position;
            position.x = (int)positionPhysically.x;
            position.y = (int)positionPhysically.z;
        
            
            
            isPlayersUnit = true;
        
            //Debug.Log("From base: " + position.x.ToString());
            //Debug.Log("From base: " + position.y.ToString());
        }

        public bool CanMoveTo(Vector2Int coordinates)
        {
            // return availableMoves.Contains(coordinates);
            return true; // For testing only
        }

        public void MoveTo(Vector2Int coordinates)
        {
            position = new Vector2Int(coordinates.x, coordinates.y);
            gameObject.transform.position = new Vector3(coordinates.x, -0.4f, coordinates.y);
        }
        
    
        public virtual void SetSelected() { }

        public virtual void SetDeselected() { }


    }
}