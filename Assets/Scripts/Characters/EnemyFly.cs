using System.Collections;
using System.Collections.Generic;
using Board;
using UnityEngine;

/*
-------------------------------------------
Flying enemy unit
-------------------------------------------
*/

namespace Characters
{
    public class EnemyFly : EnemyBase
    {
        
        protected override void Start()
        {
            base.Start();
            health = 2;
            movement = 3;
        }

        public override void AITurn()
        {
            availableMoves = board.CalculateMovableTiles(position, movement);
            Dictionary<Vector2Int, TileBase> posHitPairs = new Dictionary<Vector2Int, TileBase>();
            
            foreach (TileBase tile in availableMoves)
            {
                List<TileBase> neighbours = board.GetNeighbours(tile);
                foreach (TileBase neighbour in neighbours)
                {
                    GameObject unit = board.GetUnitOnTile(new Vector2Int(neighbour.gridX, neighbour.gridY));
                    if (!unit)
                        continue;
                    // if unit is player or building
                        // add position tile and the hittable tile to a dictionary
                    
                }
            }
            StartCoroutine(TurnCoroutine());
        }

        IEnumerator TurnCoroutine()
        {
            // Get all movable positions
            
            // Check if neighbour of every position contains a 
            
            Debug.Log(gameObject.name + " about to move...");
            yield return new WaitForSeconds(2);
            
            Debug.Log(gameObject.name + " moved.");
            yield return new WaitForSeconds(2);
            
            Debug.Log(gameObject.name + " attacked.");
            
            
        }
    }
}

