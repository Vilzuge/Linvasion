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

    public void SetGameState(GameState state)
    {
        this.state = state;
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
