using UnityEngine;

namespace Enemy.States
{
    public class IdleState: EnemyState
    {
        public IdleState(Enemy enemy, EnemyStateMachine enemyStateMachine, Animator animatorController) 
            : base(enemy, enemyStateMachine, animatorController, "Idling")
        {
        }

        public override void TransitionChecks()
        {
            base.TransitionChecks();
        }
    }
}