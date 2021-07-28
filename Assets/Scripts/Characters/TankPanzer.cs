using UnityEngine;

namespace Characters
{
    public class TankPanzer : BaseTank
    {
        // UI
        public GameObject panzerAimButton;
        

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _defaultMaterial = Resources.Load<Material>("Materials/TankPanzer");
            _selectedMaterial = Resources.Load<Material>("Materials/TankSelected");
        }

        public override void SetSelected()
        {
            base.SetSelected();
            panzerAimButton = Instantiate(aimButton, myCanvas.transform, false);
        }

        public override void SetDeselected()
        {
            base.SetDeselected();
            Destroy(panzerAimButton);
        }
    }
}
