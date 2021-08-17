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

        public virtual void AITurn() { }
        
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
