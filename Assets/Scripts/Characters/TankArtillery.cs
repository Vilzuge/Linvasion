using UnityEngine;

namespace Characters
{
    public class TankArtillery : BaseTank
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _defaultMaterial = Resources.Load<Material>("Materials/TankArtillery");
            _selectedMaterial = Resources.Load<Material>("Materials/TankSelected");
        }
    
        // Update is called once per frame
        void Update()
        {
        
        }
    
        public override void SetSelected()
        {
            GetComponent<MeshRenderer>().material = _selectedMaterial;
        }

        public override void SetDeselected()
        {
            GetComponent<MeshRenderer>().material = _defaultMaterial;
        }
    }
}
