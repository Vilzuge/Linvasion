using UnityEngine;

/*
-------------------------------------------
Rotating the skybox around the board for -cool- visuals
-------------------------------------------
*/

namespace Environment
{
    public class SkyboxRotate : MonoBehaviour
    {
        
        void Update()
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * 1.25f);
        }
    }
}
