using UnityEngine;

namespace Player.States
{
    public class VerticalAttackState : PlayerState
    {
        private float attackDuration = 0.5f; // How long the attack lasts
        private bool attackComplete = false;
        private bool hasDealtDamage = false;
        private int attackDamage = 50;
        private float attackRange = 1.5f;

        public VerticalAttackState(Player player, PlayerStateMachine playerStateMachine, Animator animatorController) 
            : base(player, playerStateMachine, animatorController, "VerticalAttacking")
        {
        }

        public override void Enter()
        {
            base.Enter();
            hasDealtDamage = false;
            attackComplete = false;
            
            // Reduce horizontal movement during attack (optional)
            Vector2 currentVelocity = player.playerRigidbody.velocity;
            player.playerRigidbody.velocity = new Vector2(currentVelocity.x * 0.5f, currentVelocity.y);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // Allow some horizontal movement during attack (reduced)
            if (player.PlayerControls.inputMove != Vector2.zero)
            {
                float moveSpeed = player.speed * 0.3f; // Reduced movement during attack
                Vector2 velocity = new Vector2(
                    player.PlayerControls.inputMove.x * moveSpeed,
                    player.playerRigidbody.velocity.y
                );
                player.playerRigidbody.velocity = velocity;
            }

            // Check if attack duration has passed
            if (Time.time - startTime >= attackDuration)
            {
                attackComplete = true;
            }
        }

        public override void TransitionChecks()
        {
            base.TransitionChecks();
            
            // Skip most transitions if airborne
            if (player.IsAirborne)
                return;

            // Priority 1: Jump (if grounded and jump pressed)
            if (player.IsGrounded() && player.PlayerControls.JumpPressed)
            {
                player.PlayerControls.ResetJump();
                PlayerStateMachine.ChangeState(player.jumpState);
                return;
            }

            // Priority 2: Attack finished or button released
            if (attackComplete || !player.PlayerControls.IsVerticalAttacking)
            {
                // Determine next state based on current input
                if (player.PlayerControls.inputMove != Vector2.zero)
                {
                    if (player.PlayerControls.IsSprinting)
                    {
                        PlayerStateMachine.ChangeState(player.runState);
                    }
                    else
                    {
                        PlayerStateMachine.ChangeState(player.walkState);
                    }
                }
                else
                {
                    PlayerStateMachine.ChangeState(player.idleState);
                }
                return;
            }
        }

        public override void Exit()
        {
            base.Exit();
            // Could add attack effect logic here
            // Example: spawn attack hitbox, play sound effect, etc.
        }

        
        public override void AnimationTrigger()
        {
            base.AnimationTrigger();
    
            if (!hasDealtDamage)
            {
                hasDealtDamage = true;
                PerformAttack();
            }
        }

        private void PerformAttack()
        {
            Debug.Log("Player attacks!");
            player.PlayHitSound();
    
            // Calculate where the attack hits
            Vector2 attackPosition = (Vector2)player.transform.position + 
                                     new Vector2(player.transform.localScale.x * 0.8f, -0.3f);
    
            // Find enemies in range
            Collider2D[] enemies = Physics2D.OverlapCircleAll(
                attackPosition, 
                attackRange, 
                LayerMask.GetMask("Enemy")
            );
    
            // Damage each enemy found
            foreach (var enemyCollider in enemies)
            {
                var enemy = enemyCollider.GetComponent<Enemy.Goblin>();
                if (enemy != null)
                {
                    enemy.TakeDamage(attackDamage);
                    Debug.Log($"Hit {enemy.name}!");
                }
            }
    
            // Show attack area (red circle in Scene view)
            
                
        }
    }
}