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

        private Board.Board board;
        private SoundManagerScript soundManager;
        protected TankState state;
    
        public List<Vector2Int> availableMoves;
        public bool isPlayersUnit;

        public Vector2Int position;
    
    
        protected virtual void Start()
        { 
            board = GameObject.Find("GameBoard").GetComponent<Board.Board>();
            soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
            myCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        
            var positionPhysically = transform.position;
            position.x = (int)positionPhysically.x;
            position.y = (int)positionPhysically.z;
            state = TankState.Unselected;
            
            isPlayersUnit = true; // dumb variable
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


        public virtual void SetSelected()
        {
            state = TankState.Moving;
            GetComponent<MeshRenderer>().material = _selectedMaterial;
        }

        public virtual void SetDeselected()
        {
            state = TankState.Unselected;
            GetComponent<MeshRenderer>().material = _defaultMaterial;
        }

        public virtual void SetAiming()
        {
            state = TankState.Aiming;
            
            //TODO: Calculate shootable tiles
            
            //TODO: Draw them with gridmanager
        }
    }
}