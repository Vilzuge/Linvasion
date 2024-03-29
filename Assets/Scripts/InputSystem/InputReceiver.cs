﻿using UnityEngine;

/*
-------------------------------------------
Base class for input receivers
-------------------------------------------
*/

namespace InputSystem
{
    public abstract class InputReceiver : MonoBehaviour
    {
        protected IInputHandler[] inputHandlers;

        public abstract void OnClickInputReceived();
        public abstract void OnHoverInputReceived();

        private void Awake()
        {
            inputHandlers = GetComponents<IInputHandler>();
        }
    }
}
