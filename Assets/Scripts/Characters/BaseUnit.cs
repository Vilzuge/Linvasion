using System.Collections.Generic;
using Board;
using SFX;
using UnityEngine;

/*
-------------------------------------------
Base class for players units
-------------------------------------------
*/

namespace Characters
{
    public class BaseUnit : MonoBehaviour, IKillable, IDamageable<int>
    {
        public Material defaultMaterial;
        public Material selectedMaterial;
        public Canvas myCanvas;
        public GameObject aimButton;

        protected Board.Board board;
        private SoundManagerScript soundManager;
        public TankState state;
        public int health;
        public int movementValue;
        public int damageValue;
    
        
        public List<TileBase> availableMoves;
        public List<TileBase> availableShots;

        public Vector2Int position;
    
    
        protected virtual void Start()
        { 
            board = GameObject.Find("GameBoard").GetComponent<Board.Board>();
            soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
            myCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        
            state = TankState.Unselected;
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
            availableMoves = CalculateMovableTiles();
            availableShots = CalculateAvailableShots();
        }
        
        public virtual List<TileBase> CalculateMovableTiles()
        {
            return board.CalculateMovableTiles(position, movementValue);
        }

        public virtual List<TileBase> CalculateAvailableShots()
        {
            return null;
        }


        public virtual void SetSelected()
        {
            state = TankState.Selected;
            GetComponent<MeshRenderer>().material = selectedMaterial;
        }

        public virtual void SetDeselected()
        {
            state = TankState.Unselected;
            GetComponent<MeshRenderer>().material = defaultMaterial;
        }

        public virtual void SetAiming()
        {
            state = TankState.Aiming;
            board.DrawShootableTiles(availableShots);
        }
        
        public virtual void TryToShoot(TileBase tileToShoot) { }
        
        
        /* INTERFACE STUFF */
        public void Kill()
        {
            Destroy(this.gameObject);
        }

        public void Damage(int damageTaken)
        {
            health -= damageTaken;
            Debug.Log(this.name + " to be killed.");
            if (health <= 0)
                Kill();
        }
    }
}
