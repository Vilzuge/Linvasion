using System.Collections;
using System.Collections.Generic;
using Board;
using SFX;
using UnityEngine;

namespace Characters
{
    
    public class UnitShooting : MonoBehaviour
    {
        
        [SerializeField] protected GameObject aimButton;
        protected BoardDrawer boardDrawer;

        void Start()
        {
            boardDrawer = GameObject.Find("GameBoard").GetComponent<BoardDrawer>();
        }

        public bool CanShootTo(TileBase tileToShoot)
        {
            return CalculateShootableTiles().Contains(tileToShoot);
        }


        public virtual List<TileBase> CalculateShootableTiles()
        {
            //return boardCalculator.CalculateShootableTiles();
            return null;
        }
        
        public virtual void SetAiming()
        {
            GetComponent<BaseUnit>().state = TankState.Aiming;
            boardDrawer.DrawShootableTiles(CalculateShootableTiles());
        }
        
        public virtual void TryToShoot(TileBase tileToShoot) { }
    }
}