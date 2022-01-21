using System.Collections.Generic;
using Board;
using UnityEngine;

namespace Characters
{
    
    public class UnitShooting : MonoBehaviour
    {
        private BoardDrawer boardDrawer;
        private BoardCalculator boardCalculator;
        [SerializeField] private int damageValue;

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

        public void TryToShoot(BaseTile tileToShoot)
        {
            if (CanShootTo(tileToShoot))
            {
                GameObject unit = boardCalculator.GetUnitOnTile(new Vector2Int(tileToShoot.gridX, tileToShoot.gridY));
                if (unit)
                {
                    unit.GetComponent<BaseUnit>().Damage(damageValue);
                }
            }
            else
            {
                Debug.Log("Could not shoot that tile..");
            }
        }
    }
}