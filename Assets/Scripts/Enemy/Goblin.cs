using System;
using Enemy.States;
using UnityEngine;

namespace Enemy
{
    public class Goblin : Enemy
    {
        [Header("Goblin Specific Settings")]
        [SerializeField] private float aggroRange = 5f;
        [SerializeField] private float attackRange = 1.5f;
        [SerializeField] private int attackDamage = 25;
        [SerializeField] private float attackCooldown = 2f;
        
        // Goblin-specific properties
        public float AggroRange => aggroRange;
        public float AttackRange => attackRange;
        public int AttackDamage => attackDamage;
        public float AttackCooldown => attackCooldown;
        
        // Player detection
        public Transform PlayerTarget { get; private set; }
        public bool IsPlayerInRange { get; private set; }
        public bool CanAttack { get; private set; } = true;
        
        // Additional states for Goblin
        public ChaseState ChaseState { get; private set; }
        public AttackState AttackState { get; private set; }
        
        private float lastAttackTime;

        protected override void Awake()
        {
            base.Awake();
            
            // Find player in scene (you might want to use a more sophisticated method)
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                PlayerTarget = playerObj.transform;
            }
        }

        protected override void Start()
        {
            base.Start();
           // Debug.Log($"{gameObject.name} Goblin is ready for battle!");
        }

        protected override void Update()
        {
            // IMPORTANT: Call base.Update() to maintain state machine functionality
            base.Update();
            
            // Goblin-specific updates
            UpdatePlayerDetection();
            UpdateAttackCooldown();
        }

        protected override void InitializeStates()
        {
            // Initialize base enemy states
            base.InitializeStates();
            
            // Add goblin-specific states
            ChaseState = new ChaseState(this, EnemyStateMachine, Animator);
            AttackState = new AttackState(this, EnemyStateMachine, Animator);
        }

        private void UpdatePlayerDetection()
        {
            if (PlayerTarget == null) return;

            float distanceToPlayer = Vector2.Distance(transform.position, PlayerTarget.position);
            IsPlayerInRange = distanceToPlayer <= aggroRange;
            
            
        }

        private void OnDrawGizmosSelected()
        {
            // Debug visualization in Scene view
            Gizmos.color = IsPlayerInRange ? Color.red : Color.yellow;
            Gizmos.DrawWireSphere(transform.position, aggroRange);
        }

        private void UpdateAttackCooldown()
        {
            if (!CanAttack && Time.time - lastAttackTime >= attackCooldown)
            {
                CanAttack = true;
            }
        }

        public void Attack()
        {
            if (!CanAttack) return;

            lastAttackTime = Time.time;
            CanAttack = false;
    
            Debug.Log("Goblin attacks!");
    
            // Calculate attack position
            Vector2 attackPosition = (Vector2)transform.position + 
                                     new Vector2(transform.localScale.x * 0.8f, 0f);
    
            // Check for player in range
            Collider2D playerCollider = Physics2D.OverlapCircle(
                attackPosition,
                attackRange,
                LayerMask.GetMask("Player")
            );

            if (playerCollider != null)
            {
                var player = playerCollider.GetComponent<Player.Player>();
                if (player != null)
                {
                    player.TakeDamage(attackDamage);
                    Debug.Log($"Goblin hits player for {attackDamage} damage!");
                }
            }
            
        }
        public float GetDirectionToPlayer()
        {
            if (PlayerTarget == null) return 0f;
            
            return Mathf.Sign(PlayerTarget.position.x - transform.position.x);
        }

        public float GetDistanceToPlayer()
        {
            if (PlayerTarget == null) return float.MaxValue;
            
            return Vector2.Distance(transform.position, PlayerTarget.position);
        }

        // Override TakeDamage to add goblin-specific behavior
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            
            // Goblin-specific damage reaction
           // Debug.Log($"Goblin takes {damage} damage! Remaining health: {Life}");
            
            // Could add damage animation, sound effects, etc.
            // Example: Play hurt animation
            // Animator.SetTrigger("Hurt");
        }

        protected override void Die()
        {
            //Debug.Log("Goblin has been defeated!");
            
            // Goblin-specific death behavior
            // Could drop items, play death animation, add score, etc.
            
            base.Die();
        }

        // Debug method to visualize goblin's detection ranges
        private void OnDrawGizmosSelectesd()
        {
            // Draw aggro range
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, aggroRange);
            
            // Draw attack range
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            
            // Draw line to player if detected
            if (PlayerTarget != null && IsPlayerInRange)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, PlayerTarget.position);
            }
        }
    }
}