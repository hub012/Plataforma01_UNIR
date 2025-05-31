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
        public float jumpSpeed = 10f;
        public float sprintMultiplier = 2f;
        
        [Header("Ground Detection")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckDistance = 0.1f;
        
        // Components
        public Rigidbody2D playerRigidbody { get; private set; }
        public Animator playerAnimator { get; private set; }
        public PlayerControls PlayerControls { get; private set; }
        
        // Properties
        public float SprintingSpeed => speed * sprintMultiplier;
        public bool IsAirborne { get; set; }
        
        // State Machine
        private PlayerStateMachine _playerStateMachine;
        
        #region Player States
        public WalkState walkState { get; private set; }
        public IdleState idleState { get; private set; }
        public RunState runState { get; private set; }
        public JumpState jumpState { get; private set; }
        public VerticalAttackState verticalAttackState { get; private set; }
        #endregion

        void Awake()
        {
            // Get components
            playerRigidbody = GetComponent<Rigidbody2D>();
            playerAnimator = GetComponent<Animator>();
            PlayerControls = GetComponent<PlayerControls>();
            
            // Initialize state machine
            _playerStateMachine = new PlayerStateMachine();
            
            // Initialize states
            InitializeStates();
            
            //Initialize health
            currentHealth = maxHealth;
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
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            Debug.Log($"Player health: {currentHealth}/{maxHealth}");
    
            if (currentHealth <= 0)
            {
                Debug.Log("Player died!");
                // Handle death later
            }
        }
    }
}