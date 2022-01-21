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
        private int damage = 1;

        protected override void Start()
        {
            base.Start();
            boardCalculator = GameObject.Find("GameBoard").GetComponent<BoardCalculator>();
        }

        /*
         * THIS IS WHOLE AI SOLUTION VERY EXPERIMENTAL AND TERRIBLE AT THE MOMENT
         */

        private class FavourablePosition
        {
            public BaseTile AttackerPosition { get; }
            public GameObject Receiver { get; }

            public FavourablePosition(BaseTile attPosition, GameObject receiver)
            {
                AttackerPosition = attPosition;
                Receiver = receiver;
            }
        }

        public override IEnumerator AITurn()
        {
            var availableMoves = GetComponent<UnitMovement>().GetAvailableMoves();
            var posHitPairs = new List<FavourablePosition>();
            
            foreach (var tile in availableMoves)
            {
                List<BaseTile> neighbours = boardCalculator.GetNeighbours(tile);
                foreach (var neighbour in neighbours)
                {
                    GameObject unit = boardCalculator.GetUnitOnTile(new Vector2Int(neighbour.gridX, neighbour.gridY));
                    if (unit)
                        if (unit.GetComponent<BaseUnitPlayer>())
                        {
                            posHitPairs.Add(new FavourablePosition(tile, unit));
                        }
                }
            }
            
            Debug.Log("Here are the available hits for " + gameObject.name);
            foreach (var pair in posHitPairs)
            {
                Debug.Log($"Attacker position: {pair.AttackerPosition}, Receiver position: {pair.Receiver}");
            }
            
            if (posHitPairs.Count > 0)
                yield return StartCoroutine(TurnCoroutine(posHitPairs));
            else
            {
                Debug.Log("No possible moves.");
            }
        }
        
        IEnumerator TurnCoroutine(List<FavourablePosition> phPairs)
        {
            Debug.Log(gameObject.name + " about to move...");
            var amountOfMoves = phPairs.Count();
            
            yield return new WaitForSeconds(2);
            if (amountOfMoves > 0)
                GetComponent<UnitMovement>().MoveTo(new Vector2Int(phPairs.First().AttackerPosition.gridX, phPairs.First().AttackerPosition.gridY));
            
            Debug.Log(gameObject.name + " moved.");
            yield return new WaitForSeconds(2);
            
            phPairs.First().Receiver.GetComponent<BaseUnit>().Damage(damage);
            
            Debug.Log(gameObject.name + " attacked.");
            yield return new WaitForSeconds(2);
        }
    }
}