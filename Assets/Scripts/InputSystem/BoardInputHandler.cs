﻿using System;
using Board;
using UnityEngine;

namespace InputSystem
{
    public class BoardInputHandler : MonoBehaviour, IInputHandler
    {
        private Board.Board board;

        private void Awake()
        {
            board = GetComponent<Board.Board>();
        }
    
        public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action callback)
        {
            board.OnTileSelected(inputPosition);
        }
    }
}
