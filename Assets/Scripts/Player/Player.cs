using Player.States;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
    
        public Rigidbody2D playerRigidbody;
        [SerializeReference] public float speed;
        [SerializeReference] public float jumpSpeed;
        [SerializeField] private LayerMask groundLayer;

        public float SprintingSpeed { get; private set; }
        public Animator playerAnimator;
        
        private float distanceToGround;
        public bool IsAirborne { get; set; } 

        private PlayerStateMachine _playerStateMachine;
        private PlayerState currentState;
    
        #region Player States
        public WalkState walkState;
        public IdleState idleState;
        public RunState runState;
        public JumpState jumpState;
        public VerticalAttackState verticalAttackState;
        #endregion
        #region Player Controls
        public PlayerControls PlayerControls{ get; private set;}
        #endregion

        void Start()
        {
            // Inputs
            PlayerControls = GetComponent<PlayerControls>(); 

            //speed init
            SprintingSpeed = speed * 2;
            playerRigidbody = GetComponent<Rigidbody2D>();
            playerAnimator = GetComponent<Animator>();
            _playerStateMachine = new PlayerStateMachine();

            distanceToGround = GetComponent<Collider2D>().bounds.extents.y;

            // Set IdleState as the initial state
            currentState = idleState = new IdleState(this, _playerStateMachine, playerAnimator);
            walkState = new WalkState(this, _playerStateMachine, playerAnimator);
            runState = new RunState(this, _playerStateMachine, playerAnimator);
            jumpState = new JumpState(this, _playerStateMachine, playerAnimator);
            verticalAttackState = new VerticalAttackState(this, _playerStateMachine, playerAnimator);
            _playerStateMachine.InitStateMachine(currentState); //Inicio la maquina con todos los estados predefenidos
    
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log("Jump " + PlayerControls.JumpPressed);
            _playerStateMachine?.CurrentState?.LogicUpdate();
            // Detectar si el jugador tiene la mas minima velocidad y si la ultima velocidad que tuvo fue negativa o positiva
            //bool playerHasMovementSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;

            FlipSprite();
            Debug.Log("Este en el suelo "+IsGrounded());
            Debug.Log("Distancia al suelo: "+distanceToGround);

        
            //if (!playerHasMovementSpeed)
        }
        void FixedUpdate(){
            _playerStateMachine?.CurrentState?.PhysicsUpdate();
        }

        /// <summary>
        /// Method <c>FlipSprite</c> Hay un truco para hacer flip del player con cambiar el scale entre -1 y 1 en el eje x
        /// Uso Mathf.Sign para solo sacar si el valor positivo o no y usarlo en base al 1
        /// Si es 0 va hacia el valor positivo es decir uno.
        /// Ejemplos: Mathf.Sign(35) = 1, Mathf.Sign(-40) = -1, Mathf.Sign(0) = 1
        /// </summary>
        void FlipSprite(){
            if (Mathf.Abs(PlayerControls.inputMove.x) > 0.01f)
            {
                float direction = Mathf.Sign(PlayerControls.inputMove.x); 
                transform.localScale = new Vector2(direction, 1f);
            }
        }

        public bool IsGrounded()
        {
            float rayLength = distanceToGround + .6f;
            Vector2 origin = transform.position;
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayLength, groundLayer);

            Debug.DrawRay(origin, Vector2.down * rayLength, Color.red); // Ver en Scene

            return hit.collider != null;
        }
    }
}