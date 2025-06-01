using System;
using Enemy.States;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy Settings")]
        [SerializeField] protected int maxLife = 100;
        [SerializeField] protected float moveSpeed = 2f;
        [SerializeField] protected float patrolDistance = 5f;
        [SerializeField] protected float idleTime = 2f;

        // Componentes
        public Rigidbody2D Rb;
        protected Animator Animator;
        
        // Propiedades
        public int Life { get; protected set; }
        public float MoveSpeed => moveSpeed;
        public float PatrolDistance => patrolDistance;
        public float IdleTime => idleTime;
        public Vector3 StartPosition { get; protected set; }
        
        public event Action OnDeath;


        // State Machine
        protected EnemyStateMachine EnemyStateMachine;
        
        #region Enemy States
        public IdleState IdleState { get; protected set; }
        public PatrolState PatrolState { get; protected set; }
        #endregion

        protected virtual void Awake()
        {
            // Get components
            Rb = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            
            // Initialize properties
            Life = maxLife;
            StartPosition = transform.position;
            
            // Initialize state machine
            EnemyStateMachine = new EnemyStateMachine();
            
            // Initialize states
            InitializeStates();
        }

        protected virtual void Start()
        {
            // Start with idle state
            EnemyStateMachine.InitStateMachine(IdleState);
           // Debug.Log(EnemyStateMachine.CurrentState);
        }

        protected virtual void Update()
        {
            // Update current state
            EnemyStateMachine.CurrentState?.LogicUpdate();
        }

        protected virtual void FixedUpdate()
        {
            // Physics updates for current state
            EnemyStateMachine.CurrentState?.PhysicsUpdate();
        }

        protected virtual void InitializeStates()
        {
            IdleState = new IdleState(this, EnemyStateMachine, Animator);
            PatrolState = new PatrolState(this, EnemyStateMachine, Animator);
        }

        public virtual void TakeDamage(int damage)
        {
            Life -= damage;
            if (Life <= 0)
            {
                OnDeath?.Invoke();
                Die();
            }
        }
        public void AnimationTrigger()
        {
            EnemyStateMachine.CurrentState?.AnimationTrigger();
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
        
        public void FlipSprite(float direction)
        {
            if (Mathf.Abs(direction) > 0.01f)
            {
                float spriteDirection = Mathf.Sign(direction);
                transform.localScale = new Vector3(spriteDirection, 1f, 1f);
            }
        }
    }
}