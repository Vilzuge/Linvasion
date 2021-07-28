using UnityEngine;

namespace Environment
{
    public class SkyboxRotate : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * 2);
            //To set the speed, just multiply Time.time with whatever amount you want.
        }
    }
}
