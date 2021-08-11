using System;
using UnityEngine;

/*
-------------------------------------------
Interface for input handlers
-------------------------------------------
*/

namespace InputSystem
{
    public interface IInputHandler
    {
        void ProcessClickInput(Vector3 inputPosition, GameObject selectedObject, Action callback);
        void ProcessHoverInput(Vector3 inputPosition, GameObject selectedObject, Action callback);
    }
}
