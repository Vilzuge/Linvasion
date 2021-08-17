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

        public void SwitchToPlayerTurn()
        {
            state = GameState.PlayerTurn;
        }

        public void ExecuteEnemyTurn()
        {
            if (!enemyUnits)
                return;

            foreach (Transform child in enemyUnits.transform)
            {
                if (child.GetComponent<EnemyBase>())
                {
                    child.GetComponent<EnemyBase>().AITurn();
                }
            }
            // if going to hit, execute hit

            // check where player/building can be hit

            // choose position

            // move to position, if no available, move towards middle

            // prepare to hit the target (Show indicator)
        }
        
        public void SetGameState(GameState stateToSet)
        {
            state = stateToSet;
        }

        public bool IsTeamTurnActive()
        {
            return state == GameState.PlayerTurn;
        }
        
    
        public bool CanPerformMove()
        {
            if (!IsTeamTurnActive())
            {
                return false;
            }
            return true;
        }
    }
}
