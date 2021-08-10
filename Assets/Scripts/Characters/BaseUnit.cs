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
    public class BaseUnit : MonoBehaviour
    {
        public Material _defaultMaterial;
        public Material _selectedMaterial;
        public Canvas myCanvas;
        public GameObject aimButton;

        protected Board.Board board;
        private SoundManagerScript soundManager;
        public TankState state;
        public int health;
        public int movementValue;
        public int damageValue;
    
        
        public List<Vector2Int> availableMoves;
        public bool isPlayersUnit;

        public Vector2Int position;
    
    
        protected virtual void Start()
        { 
            board = GameObject.Find("GameBoard").GetComponent<Board.Board>();
            soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
            myCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        
            var positionWorld = transform.position;
            position.x = (int)positionWorld.x;
            position.y = (int)positionWorld.z;
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
            state = TankState.Selected;
            GetComponent<MeshRenderer>().material = _selectedMaterial;
            foreach (Vector2Int item in availableMoves)
            {
                Debug.Log(item);
            }
        }

        public virtual void SetDeselected()
        {
            state = TankState.Unselected;
            GetComponent<MeshRenderer>().material = _defaultMaterial;
        }

        public virtual void SetAiming()
        {
            state = TankState.Aiming;
        }

        public virtual void TryToShoot(Vector2Int coords)
        {
            
        }
    }
}