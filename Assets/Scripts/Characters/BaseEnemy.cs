using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    
    public class BaseEnemy : MonoBehaviour, IKillable, IDamageable<int>
    {

        public int health;
        
        protected virtual void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        
        public void Kill()
        {
            Destroy(this);
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
