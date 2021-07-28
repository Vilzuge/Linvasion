using UnityEngine;

namespace Characters
{
    public class TankPanzer : BaseTank
    {
        // PARTICLE EFFECTS
        public ParticleSystem explosion;
        public GameObject panzerAimButton;
        

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _defaultMaterial = Resources.Load<Material>("Materials/TankPanzer");
            _selectedMaterial = Resources.Load<Material>("Materials/TankSelected");
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Pressed()
        {

        }

        public override void SetSelected()
        {
            GetComponent<MeshRenderer>().material = _selectedMaterial;
            panzerAimButton = Instantiate(aimButton, myCanvas.transform, false);
        }

        public override void SetDeselected()
        {
            GetComponent<MeshRenderer>().material = _defaultMaterial;
            Destroy(panzerAimButton);
        }
    }
}
