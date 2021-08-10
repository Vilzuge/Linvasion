using UnityEngine;

namespace Characters
{
    public class TankHarpoon : BaseUnit
    {
        // UI
        public GameObject harpoonAimButton;
        
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _defaultMaterial = Resources.Load<Material>("Materials/TankHarpoon");
            _selectedMaterial = Resources.Load<Material>("Materials/TankSelected");
            state = TankState.Unselected;
            isPlayersUnit = true;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    
        public override void SetSelected()
        {
            base.SetSelected();
            harpoonAimButton = Instantiate(aimButton, myCanvas.transform, false);
        }

        public override void SetDeselected()
        {
            base.SetDeselected();
            Destroy(harpoonAimButton);
        }
    
    }
}
