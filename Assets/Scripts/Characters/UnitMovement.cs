using System.Collections.Generic;
using Board;
using UnityEngine;

namespace Characters {
    public class UnitMovement : MonoBehaviour
    {
        protected BoardCalculator boardCalculator;
        public Vector2Int position;
        public int movementValue;
        public List<BaseTile> availableMoves;

        // Start is called before the first frame update
        void Start()
        {
            boardCalculator = GameObject.Find("GameBoard").GetComponent<BoardCalculator>();
            var positionWorld = transform.position;
            position.x = (int)positionWorld.x;
            position.y = (int)positionWorld.z;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        
        public bool CanMoveTo(BaseTile baseTileToMove)
        {
            return GetAvailableMoves().Contains(baseTileToMove);
        }
        
        public virtual List<BaseTile> GetAvailableMoves()
        {
            return boardCalculator.CalculateMovableTiles(gameObject);
        }
        
        public void MoveTo(Vector2Int coordinates)
        {
            boardCalculator.GetTile(position).walkable = true;
            position = new Vector2Int(coordinates.x, coordinates.y);
            boardCalculator.GetTile(position).walkable = false;
            gameObject.transform.position = new Vector3(coordinates.x, 0f, coordinates.y);
            availableMoves = GetAvailableMoves();
        }
        
    }

}
