using UnityEngine;

namespace Characters
{
    public class TankHarpoon : BaseTank
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _defaultMaterial = Resources.Load<Material>("Materials/TankHarpoon");
            _selectedMaterial = Resources.Load<Material>("Materials/TankSelected");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    
        public override void SetSelected()
        {
            GetComponent<MeshRenderer>().material = _selectedMaterial;
            Instantiate(aimButton);
        }

        public override void SetDeselected()
        {
            GetComponent<MeshRenderer>().material = _defaultMaterial;
            Destroy(aimButton);
        }
    
    }
}
