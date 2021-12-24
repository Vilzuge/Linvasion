using System.Collections.Generic;
using Board;
using UnityEngine;

namespace Characters
{
    
    public class UnitShooting : MonoBehaviour
    {
        private BoardDrawer boardDrawer;
        private BoardCalculator boardCalculator;

        void Start()
        {
            boardDrawer = GameObject.Find("GameBoard").GetComponent<BoardDrawer>();
            boardCalculator = GameObject.Find("GameBoard").GetComponent<BoardCalculator>();
        }

        public bool CanShootTo(BaseTile baseTileToShoot)
        {
            return GetAvailableShots().Contains(baseTileToShoot);
        }


        private List<BaseTile> GetAvailableShots()
        {
            return boardCalculator.CalculateShootableTiles(gameObject);
        }
        
        public void Aim()
        {
            boardDrawer.DrawShootableTiles(GetAvailableShots());
        }
        
        public virtual void TryToShoot(BaseTile baseTileToShoot) { }
    }
}