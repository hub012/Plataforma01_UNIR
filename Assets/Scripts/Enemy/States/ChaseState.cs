using UnityEngine;

namespace Enemy.States
{
    public class ChaseState : EnemyState
    {
        private Goblin goblin;
        private float chaseSpeed;

        public ChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine, Animator animatorController) 
            : base(enemy, enemyStateMachine, animatorController, "Walking") // Assuming you have a running animation
        {
            goblin = enemy as Goblin;
            // Chase slightly faster than normal move speed
            chaseSpeed = enemy.MoveSpeed * 1.5f;
        }

        public override void Enter()
        {
            base.Enter();
            //Debug.Log("Goblin starts chasing player!");
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            if (goblin?.PlayerTarget == null) return;

            // Move towards player
            float direction = goblin.GetDirectionToPlayer();
            Vector2 velocity = new Vector2(direction * chaseSpeed, enemy.Rb.velocity.y);
            enemy.Rb.velocity = velocity;
            
            // Flip sprite to face player
            enemy.FlipSprite(direction);
        }

        public override void TransitionChecks()
        {
            base.TransitionChecks();
            
            if (goblin == null) return;

            // If player is in attack range, switch to attack
            if (goblin.GetDistanceToPlayer() <= goblin.AttackRange && goblin.CanAttack)
            {
                EnemyStateMachine.ChangeState(goblin.AttackState);
                return;
            }

            // If player is out of aggro range, return to patrol
            if (!goblin.IsPlayerInRange)
            {
                EnemyStateMachine.ChangeState(enemy.PatrolState);
                return;
            }
        }

        public override void Exit()
        {
            base.Exit();
           // Debug.Log("Goblin stops chasing");
        }
    }
}