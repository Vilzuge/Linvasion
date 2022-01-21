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
    public abstract class BaseUnitEnemy : BaseUnit
    {
        
        

        protected override void Start()
        {
            base.Start();
        }


        public virtual IEnumerator AITurn()
        {
            yield return null;
        }


    }
}
