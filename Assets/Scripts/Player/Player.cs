using System.Collections;
using Player.States;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private int maxHealth = 100;
        private int currentHealth;
        
        [Header("Movement Settings")]
        public float speed = 5f;
        public float sprintMultiplier = 2f;
        
        [Header("Ground Detection")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckDistance = 0.1f;
        
        [Header("Damage Effects")]
        [SerializeField] private float flashDuration = 0.15f;
        [SerializeField] private Color damageColor = Color.red;
        
        [Header("Audio")]
        [SerializeField] private AudioClip damageSound;
        [SerializeField] private AudioClip deathSound;
        [SerializeField] private AudioClip attackSound;
        [SerializeField] private float damageSoundVolume = 0.7f;
        
        [Header("Jump Settings")]
        public float jumpSpeed = 15f; // Increased for snappier jump
        public float fallGravityMultiplier = 2.5f; // Makes falling faster
        public float lowJumpMultiplier = 2f; // Shorter jumps when button released early
        public float jumpCutHeight = 0.5f; // How much to cut jump when button released

        
        // Components
        public Rigidbody2D playerRigidbody { get; private set; }
        public Animator playerAnimator { get; private set; }
        public PlayerControls PlayerControls { get; private set; }
        private SpriteRenderer spriteRenderer;
        private AudioSource audioSource;
        
        // Properties
        public float SprintingSpeed => speed * sprintMultiplier;
        public bool IsAirborne { get; set; }
        // Jump properties
        private float defaultGravityScale;
        
        // State Machine
        private PlayerStateMachine _playerStateMachine;
        
        #region Player States
        public WalkState walkState { get; private set; }
        public IdleState idleState { get; private set; }
        public RunState runState { get; private set; }
        public JumpState jumpState { get; private set; }
        public VerticalAttackState verticalAttackState { get; private set; }
        #endregion
        
        // Damage effect variables
        private Color originalColor;
        private bool isFlashing = false;
        void Awake()
        {
            // Get components
            playerRigidbody = GetComponent<Rigidbody2D>();
            playerAnimator = GetComponent<Animator>();
            PlayerControls = GetComponent<PlayerControls>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            audioSource = GetComponent<AudioSource>();
            
            // If no AudioSource exists, add one
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.playOnAwake = false; // Don't play sound on start
                audioSource.spatialBlend = 0f; // 2D sound (not 3D positional)
            }
            
            // Initialize state machine
            _playerStateMachine = new PlayerStateMachine();
            
            // Store default gravity for jump modifications
            defaultGravityScale = playerRigidbody.gravityScale;
            
            // Initialize states
            InitializeStates();
            
            //Initialize health
            currentHealth = maxHealth;
            
            if (spriteRenderer != null)
            {
                originalColor = spriteRenderer.color;
                originalColor.a = 1f;
                spriteRenderer.color = originalColor;
            }
        }

        void Start()
        {
            // Start with idle state
            _playerStateMachine.InitStateMachine(idleState);
        }

        void Update()
        {
            // Update current state
            _playerStateMachine.CurrentState?.LogicUpdate();
            // Handle jump gravity modifiers
            HandleJumpGravity();
            // Handle sprite flipping
            FlipSprite();
            
            // Debug info (remove in production)
          //  Debug.Log($"Current State: {_playerStateMachine.CurrentState?.GetType().Name}");
          //  Debug.Log($"Is Grounded: {IsGrounded()}");
        }
        
        void FixedUpdate()
        {
            // Physics updates for current state
            _playerStateMachine.CurrentState?.PhysicsUpdate();
        }
        private void HandleJumpGravity()
        {
            if (IsAirborne)
            {
                // Make falling faster for snappier feel
                if (playerRigidbody.velocity.y < 0)
                {
                    playerRigidbody.gravityScale = defaultGravityScale * fallGravityMultiplier;
                }
                // Cut jump short if button released early
                else if (playerRigidbody.velocity.y > 0 && !PlayerControls.JumpPressed)
                {
                    playerRigidbody.gravityScale = defaultGravityScale * lowJumpMultiplier;
                }
                else
                {
                    playerRigidbody.gravityScale = defaultGravityScale;
                }
            }
            else
            {
                // Reset gravity when grounded
                playerRigidbody.gravityScale = defaultGravityScale;
            }
        }
        private void InitializeStates()
        {
            idleState = new IdleState(this, _playerStateMachine, playerAnimator);
            walkState = new WalkState(this, _playerStateMachine, playerAnimator);
            runState = new RunState(this, _playerStateMachine, playerAnimator);
            jumpState = new JumpState(this, _playerStateMachine, playerAnimator);
            verticalAttackState = new VerticalAttackState(this, _playerStateMachine, playerAnimator);
        }

        private void FlipSprite()
        {
            if (Mathf.Abs(PlayerControls.inputMove.x) > 0.01f)
            {
                float direction = Mathf.Sign(PlayerControls.inputMove.x);
                transform.localScale = new Vector3(direction, 1f, 1f);
            }
        }

        public bool IsGrounded()
        {
            Vector2 origin = (Vector2)transform.position + Vector2.down * 0.5f;
            float rayLength = groundCheckDistance;
            
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayLength, groundLayer);
            
            // Debug visualization
            Debug.DrawRay(origin, Vector2.down * rayLength, hit.collider != null ? Color.green : Color.red);
            
            return hit.collider != null;
        }
        public void AnimationTrigger()
        {
            _playerStateMachine.CurrentState?.AnimationTrigger();
        }
        private void PlayDamageSound()
        {
            if (audioSource != null && damageSound != null)
            {
                audioSource.PlayOneShot(damageSound, damageSoundVolume);
            }
        }
        public void PlayHitSound()
        {
            if (audioSource != null && damageSound != null)
            {
                audioSource.PlayOneShot(attackSound, damageSoundVolume);
            }
        }
        private IEnumerator DamageFlash()
        {
            if (spriteRenderer == null) yield break;
            
            isFlashing = true;
            
            // Store current color as backup
            Color backupColor = spriteRenderer.color;
            
            // Create damage color with full alpha
            Color flashColor = damageColor;
            flashColor.a = 1f; // Ensure full opacity
            
            // Flash to damage color
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            
            // Return to original color with full alpha
            originalColor.a = 1f; // Double-check alpha
            spriteRenderer.color = originalColor;
            
            isFlashing = false;
        }
        private void Die()
        {
            Debug.Log("Player died!");
            
            // Play death sound
            if (audioSource != null && deathSound != null)
            {
                audioSource.PlayOneShot(deathSound, damageSoundVolume);
            }
            
            // Handle death logic here
            // Example: Restart level, show game over screen, etc.
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            Debug.Log($"Player health: {currentHealth}/{maxHealth}");
            PlayDamageSound();
            // Start damage flash effect
            if (!isFlashing)
            {
                StartCoroutine(DamageFlash());
            }

            if (currentHealth <= 0)
            {
                Debug.Log("Player died!");
                // Handle death later
            }
        }
    }
}