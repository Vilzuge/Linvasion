using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColliderInputReceiver : InputReceiver
{
    private Vector3 clickPosition;

    private void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit))
            {
                clickPosition = hit.point;
                OnInputReceived();
            }
        }
    }

    public override void OnInputReceived()
    {
        foreach (var handler in inputHandlers)
        {
            handler.ProcessInput(clickPosition, null, null);
            Debug.Log("Handlers were called");
        }
    }
}
