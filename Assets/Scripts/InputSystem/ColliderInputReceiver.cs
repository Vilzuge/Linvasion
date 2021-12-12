using UnityEngine;
using UnityEngine.InputSystem;

/*
-------------------------------------------
Input receiver for the colliders (tiles)
-------------------------------------------
*/

namespace InputSystem
{
    public class ColliderInputReceiver : InputReceiver
    {
        private Vector3 clickPosition;
        private Vector3 mousePosition;
        //private Vector3 touchPosition;

        private void Update()
        {
            
            mousePosition = Mouse.current.position.ReadValue();
            //touchPosition = Touchscreen.current.position.ReadValue();

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Debug.Log("Click detected");
                if (Physics.Raycast(ray, out hit))
                {
                    clickPosition = hit.point;
                    OnClickInputReceived();
                }
            }
            else
            {
                if (Physics.Raycast(ray, out hit))
                    mousePosition = hit.point;
                else
                {
                    mousePosition = new Vector3(-1, -1, -1);
                }
                OnHoverInputReceived();
            }
        }

        public override void OnClickInputReceived()
        {
            Debug.Log("Click about to be handled");
            foreach (var handler in inputHandlers)
            {
                handler.ProcessClickInput(clickPosition, null, null);
            }
        }
        public override void OnHoverInputReceived()
        {
            foreach (var handler in inputHandlers)
            {
                handler.ProcessHoverInput(mousePosition, null, null);
            }
        }
    }
}
