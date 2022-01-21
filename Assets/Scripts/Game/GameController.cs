using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;

/*
-------------------------------------------
This script keeps track of the game that is being played
-------------------------------------------
*/

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        protected GameState state;
        
        public GameObject playerUnits;
        public GameObject enemyUnits;

        void Start()
        {
            
        }

        private void SwitchToPlayerTurn()
        {
            state = GameState.PlayerTurn;
        }

        public void SwitchToEnemyTurn()
        {
            state = GameState.EnemyTurn;
            ExecuteEnemyTurn();
        }

        private void ExecuteEnemyTurn()
        {
            if (!enemyUnits) return;

            StartCoroutine(StartEnemyTurn());
            SwitchToPlayerTurn();
        }

        
        /* Very WIP */
        private IEnumerator StartEnemyTurn()
        {
            foreach (Transform child in enemyUnits.transform)
            {
                if (child.GetComponent<BaseUnitEnemy>())
                {
                    yield return StartCoroutine(child.GetComponent<BaseUnitEnemy>().AITurn());
                }
            }
        }
        
        
        public void SetGameState(GameState stateToSet)
        {
            state = stateToSet;
        }

        public bool IsPlayerTurnActive()
        {
            return state == GameState.PlayerTurn;
        }
        
    }
}
