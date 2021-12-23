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

        public bool CanShootTo(TileBase tileToShoot)
        {
            return GetAvailableShots().Contains(tileToShoot);
        }


        private List<TileBase> GetAvailableShots()
        {
            return boardCalculator.CalculateShootableTiles(gameObject);
        }
        
        public void Aim()
        {
            boardDrawer.DrawShootableTiles(GetAvailableShots());
        }
        
        public virtual void TryToShoot(TileBase tileToShoot) { }
    }
}