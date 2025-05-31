using UnityEngine;

namespace Enemy.States
{
    public class IdleState : EnemyState
    {
        private float idleTimer = 0f;

        public IdleState(Enemy enemy, EnemyStateMachine enemyStateMachine, Animator animatorController) 
            : base(enemy, enemyStateMachine, animatorController, "Idling")
        {
        }

        public override void Enter()
        {
            base.Enter();
            idleTimer = 0f;
            
            // Stop movement when entering idle
            enemy.Rb.velocity = new Vector2(0, enemy.Rb.velocity.y);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            idleTimer += Time.deltaTime;
        }

        public override void TransitionChecks()
        {
            base.TransitionChecks();
            
            // Check if this is a Goblin and if player is detected
            if (enemy is Goblin goblin && goblin.IsPlayerInRange)
            {
                // If player is in attack range, attack immediately
                if (goblin.GetDistanceToPlayer() <= goblin.AttackRange && goblin.CanAttack)
                {
                    EnemyStateMachine.ChangeState(goblin.AttackState);
                }
                else
                {
                    // Otherwise chase the player
                    EnemyStateMachine.ChangeState(goblin.ChaseState);
                }
                return;
            }
            
            // Regular enemy behavior - transition to patrol state after idle time
            if (idleTimer >= enemy.IdleTime)
            {
                EnemyStateMachine.ChangeState(enemy.PatrolState);
            }
        }
    }
}