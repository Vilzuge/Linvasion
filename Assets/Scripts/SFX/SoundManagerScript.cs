using UnityEngine;

/*
-------------------------------------------
Sound manager that can be used to play soundeffects in the game
-------------------------------------------
*/

namespace SFX
{
    public class SoundManagerScript : MonoBehaviour
    {

        public AudioClip playerShoot, laserShoot, strongShoot, spawnTank;
        private AudioSource audioSrc;
        
        public void PlaySound(string clip)
        {
            audioSrc = GetComponent<AudioSource>();
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
}