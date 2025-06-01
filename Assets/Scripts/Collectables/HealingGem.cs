using UnityEngine;

namespace Collectables
{
    public class HealingGem : MonoBehaviour
    {
        [Header("Healing Settings")]
        [SerializeField] private int healAmount = 25;
    
        [Header("Effects")]
        [SerializeField] private AudioClip pickupSound;
        [SerializeField] private float soundVolume = 1f;
    
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Get player component
                var player = other.GetComponent<Player.Player>();
            
                if (player != null)
                {
                    // Heal the player
                    player.Heal(healAmount);
                
                    Debug.Log($"Player healed for {healAmount} health!");
                
                    // Play sound
                    if (pickupSound != null)
                    {
                        AudioSource.PlayClipAtPoint(pickupSound, transform.position, soundVolume);
                    }
                    
                
                    // Destroy the gem
                    Destroy(gameObject);
                }
            }
        }
    }
}