using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public AudioClip playerShoot, laserShoot, strongShoot, spawnTank;
    public static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlaySound(string clip)
    {
        switch (clip)
        {
            case "playerShoot":
                audioSrc.PlayOneShot(playerShoot);
                break;
            case "laserShoot":
                audioSrc.PlayOneShot(laserShoot);
                break;
            case "strongShoot":
                audioSrc.PlayOneShot(strongShoot);
                break;
            case "spawn":
                audioSrc.PlayOneShot(spawnTank);
                break;
        }
    }
}