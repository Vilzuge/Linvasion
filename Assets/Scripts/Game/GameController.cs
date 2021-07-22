using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
-------------------------------------------
This script keeps track of the gamestate
-------------------------------------------
*/
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
