using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class EnemyFly : BaseEnemy
    {
        
        protected override void Start()
        {
            base.Start();
            health = 2;
        }
    }
}

