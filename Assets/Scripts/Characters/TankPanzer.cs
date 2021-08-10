using UnityEngine;
using UnityEngine.UI;

namespace Characters
{
    public class TankPanzer : BaseUnit
    {
        // UI
        public GameObject panzerAimButton;
        

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _defaultMaterial = Resources.Load<Material>("Materials/TankPanzer");
            _selectedMaterial = Resources.Load<Material>("Materials/TankSelected");
            panzerAimButton = Instantiate(aimButton, myCanvas.transform, false);
            panzerAimButton.SetActive(false);
            state = TankState.Unselected;
            isPlayersUnit = true;
            movementValue = 3;
            damageValue = 1;
            health = 3;
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
            // Todo: draw shootable tiles
        }

        public override void TryToShoot(Vector2Int coords)
        {
            base.TryToShoot(coords);
            board.ApplyDamage(coords, damageValue);
            Debug.Log("You shot at " + coords);
        }
    }
}