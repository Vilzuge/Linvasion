using System.Collections.Generic;
using Board;
using UnityEngine;

namespace Characters {
    public class UnitMovement : MonoBehaviour
    {
        protected BoardCalculator boardCalculator;
        public Vector2Int position;
        public int movementValue;
        public List<TileBase> availableMoves;

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
        
        public bool CanMoveTo(TileBase tileToMove)
        {
            return GetAvailableMoves().Contains(tileToMove);
        }
        
        public virtual List<TileBase> GetAvailableMoves()
        {
            return boardCalculator.CalculateMovableTiles(gameObject);
        }
        
        public void MoveTo(Vector2Int coordinates)
        {
            position = new Vector2Int(coordinates.x, coordinates.y);

            gameObject.transform.position = new Vector3(coordinates.x, -0.4f, coordinates.y);
            availableMoves = GetAvailableMoves();
        }
        
    }

}
