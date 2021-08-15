using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
-------------------------------------------
Base class for enemies
-------------------------------------------
*/

namespace Characters
{
    public class EnemyBase : MonoBehaviour, IKillable, IDamageable<int>
    {

        public int health;
        public Vector2Int position;
        
        protected virtual void Start()
        {
            var positionWorld = transform.position;
            position.x = (int)positionWorld.x;
            position.y = (int)positionWorld.z;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        
        public void Kill()
        {
            Destroy(this.gameObject);
        }

        public void Damage(int damageTaken)
        {
            health -= damageTaken;
            Debug.Log(this.name + " to be killed.");
            if (health <= 0)
                Kill();
        }
    }
}
