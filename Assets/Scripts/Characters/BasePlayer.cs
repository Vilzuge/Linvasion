using SFX;
using UnityEngine;

namespace Characters
{
    public abstract class BasePlayer : BaseUnit
    {
        [SerializeField] protected Material defaultMaterial;
        [SerializeField] protected Material selectedMaterial;
        protected Canvas myCanvas;
        public TankState state;

        protected override void Start()
        {
            base.Start();
            soundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
            myCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
            state = TankState.Unselected;
        }

        /* Tank selection behaviour */
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
        
    }
}