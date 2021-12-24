using System.Collections;
using System.Collections.Generic;
using Board;
using UnityEngine;
using UnityEngine.UI;

/*
-------------------------------------------
Base class for enemies
-------------------------------------------
*/

namespace Characters
{
    public abstract class BaseEnemy : BaseUnit
    {
        
        

        protected override void Start()
        {
            base.Start();
        }
        

        public virtual void AITurn() { }
        
        
    }
}
