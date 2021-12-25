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
    public class EnemyFly : BaseUnitEnemy
    {
        private BoardCalculator boardCalculator;
        
        protected override void Start()
        {
            base.Start();
            boardCalculator = GameObject.Find("GameBoard").GetComponent<BoardCalculator>();
        }

        public override void AITurn()
        {
            var availableMoves = GetComponent<UnitMovement>().GetAvailableMoves();
            var posHitPairs = new Dictionary<BaseTile, BaseTile>();
            
            

            foreach (BaseTile tile in availableMoves)
            {
                List<BaseTile> neighbours = boardCalculator.GetNeighbours(tile);
                Debug.Log(neighbours);
                foreach (BaseTile neighbour in neighbours)
                {
                    GameObject unit = boardCalculator.GetUnitOnTile(new Vector2Int(neighbour.gridX, neighbour.gridY));
                    if (unit)
                        if (unit.GetComponent<BaseUnit>())
                        {
                            posHitPairs.Add(tile, neighbour);
                        }
                }
            }

            /*
            Debug.Log("Here are the available hits for " + gameObject.name);
            foreach (KeyValuePair<BaseTile, BaseTile> kvp in posHitPairs)
            {
                Debug.Log($"Position: {kvp.Key}, HitLocation: {kvp.Value}");
            }
            StartCoroutine(TurnCoroutine(posHitPairs));
            */
        }
        
        IEnumerator TurnCoroutine(Dictionary<BaseTile, BaseTile> phPairs)
        {
            Debug.Log(gameObject.name + " about to move...");
            yield return new WaitForSeconds(2);
            //if (phPairs.Count > 0)
                //MoveTo(new Vector2Int(phPairs.First().Key.gridX, phPairs.First().Key.gridX));
            
            Debug.Log(gameObject.name + " moved.");
            yield return new WaitForSeconds(2);

            Vector2Int hitPost = new Vector2Int(phPairs.First().Value.gridX, phPairs.First().Value.gridY);
            //boardController.ApplyDamage(hitPost, damage);
            
            Debug.Log(gameObject.name + " attacked.");
            yield return new WaitForSeconds(2);
        }
    }
}