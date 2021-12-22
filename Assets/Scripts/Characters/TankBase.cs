using System.Collections.Generic;
using Board;
using SFX;
using UnityEngine;
using UnityEngine.UI;

/*
-------------------------------------------
Base class for players units
-------------------------------------------
*/

namespace Characters
{
    public class TankBase : MonoBehaviour, IKillable, IDamageable<int>
    {
        public Material defaultMaterial;
        public Material selectedMaterial;
        public Canvas myCanvas;
        public GameObject aimButton;
        
        protected Board.Board board;
        protected Board.BoardCalculator boardCalculator;
        protected Board.BoardDrawer boardDrawer;
        
        private SoundManagerScript soundManager;
        public TankState state;
        public int health;
        public int movementValue;
        public int damageValue;
        
        public Image healthBar;
        public int startHealth;
        
        public List<TileBase> availableMoves;
        public List<TileBase> availableShots;

        public Vector2Int position;
    
    
        protected virtual void Start()
        { 
            board = GameObject.Find("GameBoard").GetComponent<Board.Board>();
            boardCalculator = GameObject.Find("GameBoard").GetComponent<BoardCalculator>();
            boardDrawer = GameObject.Find("GameBoard").GetComponent<BoardDrawer>();
            soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
            myCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        
            state = TankState.Unselected;
            var positionWorld = transform.position;
            position.x = (int)positionWorld.x;
            position.y = (int)positionWorld.z;
        }

        public bool CanMoveTo(TileBase tileToMove)
        {
            return availableMoves.Contains(tileToMove);
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
            return boardCalculator.CalculateMovableTiles(position, movementValue);
        }

        public virtual List<TileBase> CalculateAvailableShots()
        {
            return null;
        }


        public virtual void SetSelected()
        {
            state = TankState.Selected;
            GetComponentInChildren<MeshRenderer>().material = selectedMaterial;
        }

        public virtual void SetDeselected()
        {
            state = TankState.Unselected;
            GetComponentInChildren<MeshRenderer>().material = defaultMaterial;
        }

        public virtual void SetAiming()
        {
            state = TankState.Aiming;
            boardDrawer.DrawShootableTiles(availableShots);
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
            
            healthBar.fillAmount = (float)health / (float)startHealth;
            Debug.Log(this.name + " to be killed.");
            if (health <= 0)
                Kill();
        }
    }
}
