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
    public class TankPanzer : TankBase
    {
        // UI
        public GameObject panzerAimButton;
        

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            defaultMaterial = Resources.Load<Material>("Materials/Common");
            selectedMaterial = Resources.Load<Material>("Materials/M_TankSelected");
            panzerAimButton = Instantiate(aimButton, myCanvas.transform, false);
            panzerAimButton.SetActive(false);
            
            movementValue = 2;
            damageValue = 2;
            health = 3;
            availableMoves = CalculateMovableTiles();
            availableShots = CalculateAvailableShots();
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

        public override List<TileBase> CalculateAvailableShots()
        {
            base.CalculateAvailableShots();
            TileBase[,] tiles = board.GetTileArray();
            List<TileBase> tileList = new List<TileBase>();
            
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
                board.ApplyDamage(tilePos, damageValue);
                Debug.Log("You shot at " + tilePos);
            }
            else
                Debug.Log("You cant shoot there...");
        }
    }
}