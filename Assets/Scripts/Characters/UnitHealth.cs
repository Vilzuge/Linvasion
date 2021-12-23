using UnityEngine.UI;
using UnityEngine;

namespace Characters
{
    public class UnitHealth : MonoBehaviour
    {
        public int startHealth;
        public int currentHealth;
        public Image healthBar;
        
        void Start()
        {
            currentHealth = startHealth;
        }
    }
}
