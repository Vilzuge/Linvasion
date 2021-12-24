using System.Collections.Generic;
using Board;
using SFX;
using UnityEngine.UI;
using UnityEngine;

namespace Characters
{
    public class BasePlayer : MonoBehaviour, IKillable, IDamageable<int>
    {
        [SerializeField] protected Material defaultMaterial;
        [SerializeField] protected Material selectedMaterial;
        protected Canvas myCanvas;
        public TankState state;
        private SoundManagerScript soundManager;
        
        protected virtual void Start()
        {
            soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
            myCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
            state = TankState.Unselected;
        }

        public virtual void SetSelected()
        {
            state = TankState.Selected;
            GetComponentInChildren<MeshRenderer>().material = selectedMaterial;
        }

        public virtual void SetDeselected()
        {
            state = TankState.Unselected;
            GetComponentInChildren<MeshRenderer>().material = defaultMaterial;
        }
        
        public virtual void SetAiming()
        {
            state = TankState.Aiming;
            GetComponent<UnitShooting>().Aim(); //boardDrawer.DrawShootableTiles(CalculateShootableTiles());
        }


        /* INTERFACE STUFF */
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