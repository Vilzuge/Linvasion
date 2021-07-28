using System;
using UnityEngine;

namespace InputSystem
{
    public interface IInputHandler
    {
        void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action callback);
    }
}
