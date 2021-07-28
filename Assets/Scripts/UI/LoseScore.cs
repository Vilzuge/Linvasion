using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoseScore : MonoBehaviour
    {

        Text scoreText;
        int scoreAmount;
        // Start is called before the first frame update
        void Start()
        {
            scoreText = GetComponent<Text>();
            scoreAmount = InterfaceHandler.scoreValue;
            scoreText.text = "Score: " + scoreAmount;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
