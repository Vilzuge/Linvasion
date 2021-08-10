using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class EnemyFly : BaseUnit
    {
        protected override void Start()
        {
            base.Start();
            isPlayersUnit = false;
            movementValue = 3;
            damageValue = 1;
            health = 3;
        }
    }
}

