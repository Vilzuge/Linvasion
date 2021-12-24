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
        [SerializeField] protected Material selectedMaterial;
        protected SoundManagerScript soundManager;
        
        protected virtual void Start()
        {
            soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
        }
        
        /* INTERFACE STUFF FOR KILLABLE AND DAMAGEABLE UNITS */
        public void Kill()
        {
            Destroy(gameObject);
        }

        public void Damage(int damageTaken)
        {
            var health = GetComponent<UnitHealth>().currentHealth;
            var startHealth = GetComponent<UnitHealth>().startHealth;
            var healthBar = GetComponent<UnitHealth>().healthBar;
            
            health -= damageTaken;
            healthBar.fillAmount = (float)health / (float)startHealth;
            Debug.Log(this.name + " to be killed.");
            if (health <= 0)
                Kill();
        }
    }
}