using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            health = 3;
            movement = 4;
            damage = 2;
            startHealth = health;
        }

        public override void AITurn()
        {
            availableMoves = board.CalculateMovableTiles(position, movement);
            Dictionary<TileBase, TileBase> posHitPairs = new Dictionary<TileBase, TileBase>();
            
            foreach (TileBase tile in availableMoves)
            {
                List<TileBase> neighbours = board.GetNeighbours(tile);
                foreach (TileBase neighbour in neighbours)
                {
                    GameObject unit = board.GetUnitOnTile(new Vector2Int(neighbour.gridX, neighbour.gridY));
                    if (unit)
                        if (unit.GetComponent<TankBase>())
                        {
                            posHitPairs.Add(tile, neighbour);
                        }
                }
            }
            
            Debug.Log("Here are the available hits for " + gameObject.name);
            foreach (KeyValuePair<TileBase, TileBase> kvp in posHitPairs)
            {
                Debug.Log($"Position: {kvp.Key}, HitLocation: {kvp.Value}");
            }
            StartCoroutine(TurnCoroutine(posHitPairs));
        }

        IEnumerator TurnCoroutine(Dictionary<TileBase, TileBase> phPairs)
        {
            Debug.Log(gameObject.name + " about to move...");
            yield return new WaitForSeconds(2);
            if (phPairs.Count > 0)
                MoveTo(new Vector2Int(phPairs.First().Key.gridX, phPairs.First().Key.gridX));
            
            Debug.Log(gameObject.name + " moved.");
            yield return new WaitForSeconds(2);

            Vector2Int hitPost = new Vector2Int(phPairs.First().Value.gridX, phPairs.First().Value.gridY);
            board.ApplyDamage(hitPost, damage);
            
            Debug.Log(gameObject.name + " attacked.");
            yield return new WaitForSeconds(2);
        }
    }
}