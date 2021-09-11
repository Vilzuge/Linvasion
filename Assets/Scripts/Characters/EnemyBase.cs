using System.Collections;
using System.Collections.Generic;
using Board;
using UnityEngine;

/*
-------------------------------------------
Base class for enemies
-------------------------------------------
*/

namespace Characters
{
    public class EnemyBase : MonoBehaviour, IKillable, IDamageable<int>
    {
        protected Board.Board board;
        public int health;
        public int movement;
        public Vector2Int position;
        public List<TileBase> availableMoves;
        
        protected virtual void Start()
        {
            board = GameObject.Find("GameBoard").GetComponent<Board.Board>();
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
            Debug.Log(gameObject.name + " to be killed.");
            if (health <= 0)
                Kill();
        }
    }
}
