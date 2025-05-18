using UnityEngine;

namespace Enemy.States
{
    public class PatrolState: EnemyState
    {
        public PatrolState(Enemy enemy, EnemyStateMachine enemyStateMachine, Animator animatorController) 
            : base(enemy, enemyStateMachine, animatorController, "Walking")
        {
        }

        public override void TransitionChecks()
        {
            base.TransitionChecks();
        }
    }
}