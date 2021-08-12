using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
-------------------------------------------
Flying enemy unit
-------------------------------------------
*/

namespace Characters
{
    public class EnemyFly : EnemyBase
    {
        
        protected override void Start()
        {
            base.Start();
            health = 2;
        }
    }
}

