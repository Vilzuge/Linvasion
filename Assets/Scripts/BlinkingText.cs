using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        StartBlinking();
    }
    IEnumerator Blink()
    {
        while (true)
        {
            text.text = "Choose spawning points by selecting units";
            yield return new WaitForSeconds(1f);
            text.text = " ";
            yield return new WaitForSeconds(1f);
        }
    }
    void StartBlinking()
    {
        StartCoroutine("Blink");
    }
    void StopBlinking()
    {
        StopCoroutine("Blink");
    }
    // Update is called once per frame
    void Update()
    {

    }
}