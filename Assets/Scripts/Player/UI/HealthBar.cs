using UnityEngine;
using UnityEngine.UI;

namespace Player.UI
{
    public class HealthBar : MonoBehaviour
    {
        [Header("Health Bar Settings")]
        [SerializeField] private Image healthBarFill; // The "full" bar image
        [SerializeField] private Image healthBarBackground; // The "empty" bar image
        [SerializeField] private bool smoothTransition = true;
        [SerializeField] private float transitionSpeed = 5f;
    
        private float currentHealthPercent = 1f; // 1 = 100% health
        private float targetHealthPercent = 1f;

        void Start()
        {
            // Initialize health bar to full
            if (healthBarFill != null)
            {
                healthBarFill.fillAmount = 1f;
            }
        }

        void Update()
        {
            UpdateHealthColor();
            // Smooth transition animation
            if (smoothTransition && Mathf.Abs(currentHealthPercent - targetHealthPercent) > 0.01f)
            {
                currentHealthPercent = Mathf.Lerp(currentHealthPercent, targetHealthPercent, Time.deltaTime * transitionSpeed);
            
                if (healthBarFill != null)
                {
                    healthBarFill.fillAmount = currentHealthPercent;
                }
            }
        }

        /// <summary>
        /// Update health bar display
        /// </summary>
        /// <param name="currentHealth">Current health value</param>
        /// <param name="maxHealth">Maximum health value</param>
        public void UpdateHealthBar(int currentHealth, int maxHealth)
        {
            if (maxHealth <= 0) return;

            targetHealthPercent = (float)currentHealth / maxHealth;
            targetHealthPercent = Mathf.Clamp01(targetHealthPercent); // Keep between 0 and 1

            if (!smoothTransition)
            {
                currentHealthPercent = targetHealthPercent;
                if (healthBarFill != null)
                {
                    healthBarFill.fillAmount = currentHealthPercent;
                }
            }
        }

        /// <summary>
        /// Set health bar immediately without animation
        /// </summary>
        public void SetHealthBarImmediate(float healthPercent)
        {
            healthPercent = Mathf.Clamp01(healthPercent);
            currentHealthPercent = targetHealthPercent = healthPercent;
        
            if (healthBarFill != null)
            {
                healthBarFill.fillAmount = currentHealthPercent;
            }
        }
    
        private void UpdateHealthColor()
        {
            if (healthBarFill == null) return;
    
            if (currentHealthPercent > 0.7f)
                healthBarFill.color = Color.green;
            else if (currentHealthPercent > 0.3f)
                healthBarFill.color = Color.yellow;
            else
                healthBarFill.color = Color.red;
        }

        /// <summary>
        /// Get current health percentage (0-1)
        /// </summary>
        public float GetCurrentHealthPercent()
        {
            return currentHealthPercent;
        }
    }
}