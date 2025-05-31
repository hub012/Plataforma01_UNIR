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
            
            //Debug.Log("Goblin attacks!");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            // Attacking using animation event
        }

        private void PerformAttack()
        {
            hasAttacked = true;
            //Debug.Log("Goblin attacks!");
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

                // Check what to do next
                if (goblin.IsPlayerInRange && goblin.GetDistanceToPlayer() > goblin.AttackRange)
                {
                    // Player is still in aggro range but too far to attack - chase
                    EnemyStateMachine.ChangeState(goblin.ChaseState);
                }
                else if (goblin.IsPlayerInRange && goblin.GetDistanceToPlayer() <= goblin.AttackRange && goblin.CanAttack)
                {
                    // Player still in attack range and we can attack again - attack again
                    EnemyStateMachine.ChangeState(goblin.AttackState);
                }
                else if (!goblin.IsPlayerInRange)
                {
                    // Player left aggro range - return to patrol
                    EnemyStateMachine.ChangeState(enemy.PatrolState);
                }
                else
                {
                    // Wait (attack cooldown) - go to idle briefly
                    EnemyStateMachine.ChangeState(enemy.IdleState);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        // This could be called by animation events for more precise timing
        public override void AnimationTrigger()
        {
            base.AnimationTrigger();
            
            // Usado en el animation
            if (!hasAttacked)
            {
                PerformAttack();
            }
        }
    }
}