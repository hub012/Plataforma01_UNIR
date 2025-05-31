using UnityEngine;

namespace Enemy.States
{
    public class PatrolState : EnemyState
    {
        private Vector3 leftBound;
        private Vector3 rightBound;
        private int currentDirection = 1; // 1 for right, -1 for left
        private float patrolTimer = 0f;
        private Goblin goblin;

        public PatrolState(Enemy enemy, EnemyStateMachine enemyStateMachine, Animator animatorController) 
            : base(enemy, enemyStateMachine, animatorController, "Walking")
        {
            goblin = enemy as Goblin;
        }

        public override void Enter()
        {
            base.Enter();
            
            // Set patrol bounds based on enemy's start position and patrol distance
            leftBound = enemy.StartPosition + Vector3.left * enemy.PatrolDistance;
            rightBound = enemy.StartPosition + Vector3.right * enemy.PatrolDistance;
            
            // Determine initial direction based on current position
            if (enemy.transform.position.x <= leftBound.x)
            {
                currentDirection = 1; // Move right
            }
            else if (enemy.transform.position.x >= rightBound.x)
            {
                currentDirection = -1; // Move left
            }
            
            patrolTimer = 0f;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            patrolTimer += Time.deltaTime;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            // Move the enemy
            Vector2 velocity = new Vector2(currentDirection * enemy.MoveSpeed, enemy.Rb.velocity.y);
            enemy.Rb.velocity = velocity;
            
            // Flip sprite based on direction
            enemy.FlipSprite(currentDirection);
            
            // Check if we need to turn around
            CheckBounds();
        }

        private void CheckBounds()
        {
            if (currentDirection == 1 && enemy.transform.position.x >= rightBound.x)
            {
                // Reached right bound, turn left
                currentDirection = -1;
            }
            else if (currentDirection == -1 && enemy.transform.position.x <= leftBound.x)
            {
                // Reached left bound, turn right
                currentDirection = 1;
            }
        }

        public override void TransitionChecks()
        {
            base.TransitionChecks();
            if (goblin == null) return;
            // If player is out of aggro range, return to patrol
            Debug.Log(goblin.GetDistanceToPlayer());
            if (goblin.IsPlayerInRange)
            {
                EnemyStateMachine.ChangeState(goblin.ChaseState);
                return;
            }
            
            // Transition to idle state after patrolling for a while
            // You can customize this logic based on your game needs
            if (patrolTimer >= 10f) // Patrol for 10 seconds then idle
            {
                EnemyStateMachine.ChangeState(enemy.IdleState);
            }
            
        }
    }
}