using System;
using UnityEngine;

namespace InputSystem
{
    public interface IInputHandler
    {
        void ProcessClickInput(Vector3 inputPosition, GameObject selectedObject, Action callback);
        void ProcessHoverInput(Vector3 inputPosition, GameObject selectedObject, Action callback);
    }
}
