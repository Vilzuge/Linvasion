using UnityEngine;

/*
-------------------------------------------
Harpoon tank
-------------------------------------------
*/

namespace Characters
{
    public class TankHarpoon : TankBase
    {
        // UI
        public GameObject harpoonAimButton;
        
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            defaultMaterial = Resources.Load<Material>("Materials/M_TankHarpoon");
            selectedMaterial = Resources.Load<Material>("Materials/M_TankSelected");
            state = TankState.Unselected;
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
