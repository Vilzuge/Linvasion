using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
    void processInput(Vector3 inputPosition, GameObject selectedObject, Action callback);
}
