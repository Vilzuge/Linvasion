using System;
using Board;
using UnityEngine;

/*
-------------------------------------------
Input handler for the gameboard
-------------------------------------------
*/

namespace InputSystem
{
    public class BoardInputHandler : MonoBehaviour, IInputHandler
    {
        [SerializeField] private Board.BoardController boardController;
        
        public void ProcessClickInput(Vector3 inputPosition, GameObject selectedObject, Action callback)
        {
            boardController.OnTileSelected(inputPosition);
        }
        
        public void ProcessHoverInput(Vector3 inputPosition, GameObject selectedObject, Action callback)
        {
            boardController.TrackMousePosition(inputPosition);
        }
    }
}
