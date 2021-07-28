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
        public ParticleSystem explosionEffect;
    
        private GridManager gridManager;
        private SoundManagerScript soundManager;
    
        public List<Vector2Int> availableMoves;
        public bool isPlayersUnit;

        public Vector2Int position;
    
    
        protected virtual void Start()
        { 
            gridManager = GameObject.Find("GameBoard").GetComponent<GridManager>();
            soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
        
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
    
        public virtual void SetSelected() { }

        public virtual void SetDeselected() { }


    }
}