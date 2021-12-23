using System.Collections.Generic;
using Board;
using SFX;
using UnityEngine.UI;
using UnityEngine;

namespace Characters
{
    public class BaseUnit : MonoBehaviour, IKillable, IDamageable<int>
    {
        [SerializeField] protected Material defaultMaterial;
        [SerializeField] protected Material selectedMaterial;
        protected Canvas myCanvas;
        public TankState state;
        private SoundManagerScript soundManager;

        public int health;
        public Image healthBar;
        public int startHealth;
        
        
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


        /* INTERFACE STUFF */
        public void Kill()
        {
            Destroy(this.gameObject);
        }

        public void Damage(int damageTaken)
        {
            health -= damageTaken;
            
            healthBar.fillAmount = (float)health / (float)startHealth;
            Debug.Log(this.name + " to be killed.");
            if (health <= 0)
                Kill();
        }
    }
}