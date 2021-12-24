using System.Collections;
using System.Collections.Generic;
using Board;
using UnityEngine;
using UnityEngine.UI;

/*
-------------------------------------------
Base class for enemies
-------------------------------------------
*/

namespace Characters
{
    public class BaseEnemy : MonoBehaviour, IKillable, IDamageable<int>
    {
        protected Board.BoardCalculator boardCalculator;
        protected Board.BoardController boardController;
        public int health;
        public int movement;
        public int damage;

        public Image healthBar;
        public int startHealth;
        
        public Vector2Int position;
        public List<TileBase> availableMoves;

        protected virtual void Start()
        {
            boardCalculator = GameObject.Find("GameBoard").GetComponent<Board.BoardCalculator>();
            var positionWorld = transform.position;
            position.x = (int)positionWorld.x;
            position.y = (int)positionWorld.z;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        protected void MoveTo(Vector2Int coordinates)
        {
            position = new Vector2Int(coordinates.x, coordinates.y);
            gameObject.transform.position = new Vector3(coordinates.x, 0f, coordinates.y);
        }

        public virtual void AITurn() { }
        
        public void Kill()
        {
            Debug.Log(gameObject.name + " was destroyed.");
            Destroy(gameObject);
        }

        public void Damage(int damageTaken)
        {
            health -= damageTaken;
            healthBar.fillAmount = (float)health / (float)startHealth;
            Debug.Log(gameObject.name + " to be killed.");
            if (health <= 0)
                Kill();
        }
    }
}
