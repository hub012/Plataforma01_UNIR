using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Enemy.UI
{
    public class GoblinHealthBar : MonoBehaviour
    {
        [Header("Health Bar Settings")]
        [SerializeField] private GameObject healthBarCanvas;
        [SerializeField] private Image healthBarFill;
        [SerializeField] private Image healthBarBackground;
        [SerializeField] private float hideDelay = 3f;
        [SerializeField] private float fadeSpeed = 2f;
        
        [Header("Position Settings")]
        [SerializeField] private Vector3 offset = new Vector3(0, 1f, 0);
        
        private Goblin goblin;
        private Camera mainCamera;
        private Coroutine hideCoroutine;
        private CanvasGroup canvasGroup;
        private float maxHealth;
        
        private void Awake()
        {
            goblin = GetComponentInParent<Goblin>();
            mainCamera = Camera.main;
            
            // Get or add CanvasGroup for fading
            canvasGroup = healthBarCanvas.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = healthBarCanvas.AddComponent<CanvasGroup>();
            }

            healthBarCanvas.layer = 10;
            
            // Initially hide the health bar
            healthBarCanvas.SetActive(false);
            
            canvasGroup.alpha = 0f;
            
            // Store max health
            if (goblin != null)
            {
                maxHealth = goblin.Life;
            }
        }
        
        private void LateUpdate()
        {
            if (healthBarCanvas.activeSelf && mainCamera != null)
            {
                // Make health bar face the camera
                healthBarCanvas.transform.position = goblin.transform.position + offset;
                healthBarCanvas.transform.rotation = Quaternion.LookRotation(
                    healthBarCanvas.transform.position - mainCamera.transform.position
                );
            }
        }
        
        public void UpdateHealthBar(int currentHealth)
        {
            if (goblin == null) return;
            
            // Show health bar
            ShowHealthBar();
            
            // Update fill amount
            float healthPercentage = (float)currentHealth / maxHealth;
            healthBarFill.fillAmount = Mathf.Clamp01(healthPercentage);
            
            
            // Reset hide timer
            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
            }
            hideCoroutine = StartCoroutine(HideHealthBarAfterDelay());
        }
        
        private void ShowHealthBar()
        {
            if (!healthBarCanvas.activeSelf)
            {
                healthBarCanvas.SetActive(true);
                StartCoroutine(FadeIn());
            }
        }
        
        private IEnumerator FadeIn()
        {
            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += fadeSpeed * Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 1f;
        }
        
        private IEnumerator HideHealthBarAfterDelay()
        {
            yield return new WaitForSeconds(hideDelay);
            yield return StartCoroutine(FadeOut());
            healthBarCanvas.SetActive(false);
        }
        
        private IEnumerator FadeOut()
        {
            while (canvasGroup.alpha > 0f)
            {
                canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 0f;
        }
        
        public void HideHealthBarImmediately()
        {
            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
            }
            StopAllCoroutines();
            canvasGroup.alpha = 0f;
            healthBarCanvas.SetActive(false);
        }
    }
}