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
        protected GameState state;
        protected bool isPlayersTurn;

        void Start()
        {
            isPlayersTurn = true;
        }
        public void SetGameState(GameState stateToSet)
        {
            state = stateToSet;
        }

        public bool IsTeamTurnActive()
        {
            return isPlayersTurn;
        }

        private bool IsGameInProgress()
        {
            return state == GameState.Play;
        }
    
    
        public bool CanPerformMove()
        {
            if (!IsGameInProgress())
            {
                return false;
            }
            return true;
        }
    }
}
