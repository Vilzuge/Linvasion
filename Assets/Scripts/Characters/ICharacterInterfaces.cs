using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
-------------------------------------------
Interfaces for damaging and killing units and enemies
-------------------------------------------
*/

namespace Characters
{
    public interface IKillable
    {
        void Kill();
    }

    public interface IDamageable<T>
    {
        void Damage(T damageTaken);
    }
}