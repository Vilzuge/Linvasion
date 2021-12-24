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
    public abstract class BaseEnemy : BaseUnit
    {
        
        
        protected Board.BoardCalculator boardCalculator;
        protected Board.BoardController boardController;
        public int health;
        public int movement;
        public int damage;

        public Image healthBar;
        public int startHealth;
        
        public Vector2Int position;
        public List<BaseTile> availableMoves;

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
        
        
    }
}
