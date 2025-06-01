using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class TutorialManger: MonoBehaviour
    {
        [System.Serializable]
        public class TutorialStep
        {
            public string stepName;
            public string instruction;
            public Vector3 triggerPosition;
            public float triggerRadius = 2f;
            public bool requiresAction = false;
            public string actionRequired = "";
            public bool completed = false;
            public GameObject highlightTarget;
            public float displayDuration = 5f;
        }
        [Header("Tutorial UI")]
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] private TextMeshProUGUI instructionText;
        [SerializeField] private Image instructionBackground;
        [SerializeField] private float fadeSpeed = 2f;
        
        [Header("Tutorial Steps")]
        [SerializeField] private List<TutorialStep> tutorialSteps = new List<TutorialStep>();
        [SerializeField] private int currentStep = 0;
        
        [Header("References")]
        [SerializeField] private Transform player;
        [SerializeField] private Player.PlayerControls playerControls;
        
        private Coroutine currentDisplayCoroutine;
        private bool tutorialActive = true;
        private GameObject currentArrow;
        private GameObject currentHighlight;
        
        // Input tracking
        private bool hasJumped = false;
        private bool hasAttacked = false;
        private bool hasSprinted = false;
        private bool hasMovedLeft = false;
        private bool hasMovedRight = false;
    
        private void Start()
        {
            if (tutorialActive && tutorialSteps.Count > 0)
            {
                StartCoroutine(RunTutorial());
            }
        }

        private void Awake()
        {
           
        
            // Initially hide tutorial panel
            if (tutorialPanel != null)
            {
                tutorialPanel.SetActive(false);
            }
        }

        public void SkipTutorial()
        {
            tutorialActive = false;
            StopAllCoroutines();
            if (tutorialPanel != null) tutorialPanel.SetActive(false);
            
        }
        private void Update()
        {
            if (!tutorialActive || playerControls == null) return;
        
            // Track player inputs
            if (playerControls.HasMovementInput)
            {
                if (playerControls.inputMove.x > 0) hasMovedRight = true;
                if (playerControls.inputMove.x < 0) hasMovedLeft = true;
            }
        
            if (playerControls.JumpPressed) hasJumped = true;
            if (playerControls.IsSprinting && playerControls.HasMovementInput) hasSprinted = true;
            if (playerControls.IsVerticalAttacking) hasAttacked = true;
        }
        private void ShowInstruction(string text)
        {
            if (tutorialPanel == null || instructionText == null) return;
        
            tutorialPanel.SetActive(true);
            instructionText.text = text;
        
            if (currentDisplayCoroutine != null)
            {
                StopCoroutine(currentDisplayCoroutine);
            }
        
            currentDisplayCoroutine = StartCoroutine(FadeInPanel());
        }
        private IEnumerator FadeInPanel()
        {
            float alpha = 0f;
        
            while (alpha < 1f)
            {
                alpha += Time.deltaTime * fadeSpeed;
                SetPanelAlpha(alpha);
                yield return null;
            }
        
            SetPanelAlpha(1f);
        }
        private void SetPanelAlpha(float alpha)
        {
            if (instructionBackground != null)
            {
                Color bgColor = instructionBackground.color;
                bgColor.a = alpha * 0.8f; // Slightly transparent background
                instructionBackground.color = bgColor;
            }
        
            if (instructionText != null)
            {
                Color textColor = instructionText.color;
                textColor.a = alpha;
                instructionText.color = textColor;
            }
        }

        private IEnumerator RunTutorial()
        {
            Debug.Log("Tutorial started");
            Debug.Log("Tutorial step count: " + tutorialSteps.Count);
            
            while (currentStep < tutorialSteps.Count && tutorialActive)
            {
                Debug.Log("Tutorial step count: " + currentStep);
                TutorialStep step = tutorialSteps[currentStep];
                Debug.Log("Tutorial step: " + step);
            
                // Wait for player to reach trigger position
                while (Vector3.Distance(player.position, step.triggerPosition) > step.triggerRadius)
                {
                    yield return null;
                }
            
                // Show instruction
                ShowInstruction(step.instruction);
            
              
            
                // Wait for action if required
                if (step.requiresAction)
                {
                    yield return StartCoroutine(WaitForAction(step.actionRequired));
                }
                else if (step.displayDuration > 0)
                {
                    // Just wait for duration
                    yield return new WaitForSeconds(step.displayDuration);
                }
              
                // Mark step as completed
                step.completed = true;
            
                // Hide instruction
                yield return StartCoroutine(HideInstruction());
            
                
            
                // Move to next step
                currentStep++;
            
                // Small delay between steps
                yield return new WaitForSeconds(0.5f);
            }
        
           
        }

        public void MarkStepTextAsCompleted()
        {
            instructionText.color = Color.green;
        }
        public void MarkStepTextAsStarted()
        {
            instructionText.color = Color.white;
        }
        private IEnumerator HideInstruction()
        {
            float alpha = 1f;
        
            while (alpha > 0f)
            {
                alpha -= Time.deltaTime * fadeSpeed;
                //SetPanelAlpha(alpha);
                yield return null;
            }
        
           // SetPanelAlpha(0f);
            tutorialPanel.SetActive(false);
        }

        private IEnumerator WaitForAction(string action)
        {
            bool actionCompleted = false;
        
            while (!actionCompleted)
            {
                switch (action)
                {
                    case "Move":
                        actionCompleted = hasMovedLeft && hasMovedRight;
                        break;
                    case "Jump":
                        actionCompleted = hasJumped;
                        break;
                    case "Sprint":
                        actionCompleted = hasSprinted;
                        break;
                    case "Attack":
                        actionCompleted = hasAttacked;
                        break;
                }
            
                yield return null;
            }
        
            // Show success feedback
            StartCoroutine(ShowSuccessFeedback());
        }
        private IEnumerator ShowSuccessFeedback()
        {
            string originalText = instructionText.text;
            Color originalColor = instructionText.color;
        
            instructionText.text = "Perfectoo!";
            instructionText.color = Color.green;
        
            yield return new WaitForSeconds(1f);
        
            instructionText.text = originalText;
            instructionText.color = originalColor;
        }



        private void OnDrawGizmos()
        {
            if (tutorialSteps == null) return;
        
            foreach (var step in tutorialSteps)
            {
                Gizmos.color = step.completed ? Color.green : Color.yellow;
                Gizmos.DrawWireSphere(step.triggerPosition, step.triggerRadius);
            
                
                #if UNITY_EDITOR
                UnityEditor.Handles.Label(step.triggerPosition + Vector3.up, step.stepName);
                #endif
            }
        }
    }
}