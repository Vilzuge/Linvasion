using SFX;
using UnityEngine;

namespace Characters
{
    public abstract class BaseUnitPlayer : BaseUnit
    {
        [SerializeField] protected Material selectedMaterial;
        [SerializeField] protected MeshRenderer meshRenderer;
        
        protected Canvas myCanvas;
        public TankState state = TankState.Unselected;

        protected override void Start()
        {
            base.Start();
            myCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        }

        /* Tank selection behaviour */
        public virtual void SetSelected()
        {
            state = TankState.Selected;
            meshRenderer.material = selectedMaterial;
        }

        public virtual void SetDeselected()
        {
            state = TankState.Unselected;
            meshRenderer.material = defaultMaterial;
        }

        public virtual void SetAiming()
        {
            state = TankState.Aiming;
            GetComponent<UnitShooting>().Aim();
        }
    }
}