using UnityEngine;

/*
-------------------------------------------
Artillery unit
-------------------------------------------
*/

namespace Characters
{
    public class TankArtillery : TankBase
    {
        // UI
        public GameObject artilleryAimButton;
        
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            defaultMaterial = Resources.Load<Material>("Materials/TankArtillery");
            selectedMaterial = Resources.Load<Material>("Materials/TankSelected");
            state = TankState.Unselected;
        }
    
        // Update is called once per frame
        void Update()
        {
        
        }
    
        public override void SetSelected()
        {
            base.SetSelected();
            artilleryAimButton = Instantiate(aimButton, myCanvas.transform, false);
        }

        public override void SetDeselected()
        {
            base.SetDeselected();
            Destroy(artilleryAimButton);
        }
    }
}