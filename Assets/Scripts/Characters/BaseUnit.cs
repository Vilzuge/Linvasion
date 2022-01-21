using System.Collections.Generic;
using Board;
using SFX;
using UnityEngine.UI;
using UnityEngine;

namespace Characters
{
    public abstract class BaseUnit : MonoBehaviour, IKillable, IDamageable<int>
    {
        [SerializeField] protected Material defaultMaterial;

        
        protected virtual void Start()
        {

        }
        
        /* INTERFACE STUFF FOR KILLABLE AND DAMAGEABLE UNITS */
        public void Kill()
        {
            Destroy(gameObject);
        }

        public void Damage(int damageTaken)
        {
            var unitHealth = GetComponent<UnitHealth>();
            var healthBar = unitHealth.healthBar;
            var newHealth = unitHealth.currentHealth -= damageTaken;

            healthBar.fillAmount = (float) newHealth / (float) unitHealth.startHealth;
            Debug.Log(name + " to be killed.");
            if (newHealth <= 0)
            {
                Kill();
            }
        }
    }
}