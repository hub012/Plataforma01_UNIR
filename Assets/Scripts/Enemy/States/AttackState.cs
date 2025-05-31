using UnityEngine;

namespace Enemy.States
{
    public class AttackState : EnemyState
    {
        private Goblin goblin;
        private bool hasAttacked = false;
        private float attackDuration = .9F;

        public AttackState(Enemy enemy, EnemyStateMachine enemyStateMachine, Animator animatorController) 
            : base(enemy, enemyStateMachine, animatorController, "Attacking")
        {
            goblin = enemy as Goblin;
        }

        public override void Enter()
        {
            base.Enter();
            hasAttacked = false;
            
            // Stop movement during attack
            enemy.Rb.velocity = new Vector2(0, enemy.Rb.velocity.y);
            
            // Face the player
            if (goblin?.PlayerTarget != null)
            {
                float direction = goblin.GetDirectionToPlayer();
                enemy.FlipSprite(direction);
            }
        }
        
        private void PerformAttack()
        {
            hasAttacked = true;
            goblin?.Attack();
        }

        public override void TransitionChecks()
        {
            base.TransitionChecks();
            float timeInState = Time.time - startTime;

            // Exit attack state after duration
            if (Time.time - startTime >= attackDuration)
            {
                if (goblin == null)
                {
                    EnemyStateMachine.ChangeState(enemy.IdleState);
                    return;
                }
                
                float distanceToPlayer = goblin.GetDistanceToPlayer();
        
                if (!goblin.IsPlayerInRange)
                {
                    // Player left aggro range - return to patrol
                    EnemyStateMachine.ChangeState(enemy.PatrolState);
                }
                else if (distanceToPlayer <= goblin.AttackRange && goblin.CanAttack)
                {
                    // Player in attack range AND can attack - attack again
                    EnemyStateMachine.ChangeState(goblin.AttackState);
                }
                else if (distanceToPlayer > goblin.AttackRange)
                {
                    // Player in aggro range but too far to attack - chase
                    EnemyStateMachine.ChangeState(goblin.ChaseState);
                }
                else
                {
                    // Player in attack range but can't attack (cooldown) - wait in idle
                    EnemyStateMachine.ChangeState(enemy.IdleState);
                }
            }
        }
        // Esto lo llamo desde el animation event
        public override void AnimationTrigger()
        {
            base.AnimationTrigger();
            
            if (!hasAttacked)
            {
                PerformAttack();
            }
        }
    }
}