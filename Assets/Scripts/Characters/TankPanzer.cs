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

        // UI
        [SerializeField] private GameObject panzerAimButton;
        
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            panzerAimButton = Instantiate(panzerAimButton, myCanvas.transform, false);
            panzerAimButton.SetActive(false);
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
        
        
    }
}