using UnityEngine;

namespace Enemy.States
{
    public class MovingStates: EnemyState
    {
        public MovingStates(Enemy enemy, EnemyStateMachine enemyStateMachine, Animator animatorController) 
            : base(enemy, enemyStateMachine, animatorController, "walking")
        {
        }
    }
}