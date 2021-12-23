using System.Collections.Generic;
using Board;
using UnityEngine;
using UnityEngine.UI;

/*
-------------------------------------------
Regular panzer tank
-------------------------------------------
*/

namespace Characters
{
    public class TankPanzer : BaseUnit
    {
        /*
        // UI
        public GameObject panzerAimButton;
        
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            panzerAimButton = Instantiate(aimButton, myCanvas.transform, false);
            panzerAimButton.SetActive(false);
            
            damageValue = 2;
            health = 3;
            availableMoves = CalculateMovableTiles();
            availableShots = CalculateShootableTiles();
            startHealth = health;
        }

        public override void SetSelected()
        {
            base.SetSelected();
            panzerAimButton.SetActive(true);
            panzerAimButton.GetComponent<Button>().onClick.AddListener(SetAiming);
        }

        public override void SetDeselected()
        {
            base.SetDeselected();
            panzerAimButton.SetActive(false);
        }

        public override void SetAiming()
        {
            base.SetAiming();
            Debug.Log("You are currently aiming");
        }

        public override List<TileBase> CalculateShootableTiles()
        {
            base.CalculateShootableTiles();
            TileBase[,] tiles = boardCalculator.GetTileArray();
            List<TileBase> tileList = new List<TileBase>();

            if (tiles == null) return null;
            foreach (TileBase tile in tiles)
            {
                if (tile.gridX == position.x || tile.gridY == position.y)
                {
                    if (tile.gridX == position.x && tile.gridY == position.y)
                        continue;
                    tileList.Add(tile);
                }
            }
            return tileList;
        }

        public override void TryToShoot(TileBase tileToShoot)
        {
            base.TryToShoot(tileToShoot);
            Vector2Int tilePos = new Vector2Int(tileToShoot.gridX, tileToShoot.gridY);

            if (availableShots.Contains(tileToShoot))
            {
                boardController.ApplyDamage(tilePos, damageValue);
                Debug.Log("You shot at " + tilePos);
            }
            else
                Debug.Log("You cant shoot there...");
        }
        */
    }
}