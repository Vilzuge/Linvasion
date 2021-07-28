using UnityEngine;

namespace InputSystem
{
    public abstract class InputReceiver : MonoBehaviour
    {
        protected IInputHandler[] inputHandlers;

        public abstract void OnInputReceived();

        private void Awake()
        {
            inputHandlers = GetComponents<IInputHandler>();
        }
    }
}
