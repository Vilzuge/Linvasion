using UnityEngine;

/*
-------------------------------------------
This script keeps track of the gamestate
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
            isPlayersTurn = true; // FOR TESTING
        }
        public void SetGameState(GameState state)
        {
            this.state = state;
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
