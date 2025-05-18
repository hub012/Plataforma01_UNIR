using UnityEngine;

namespace Enemy.States
{
    public class EnemyState
    {
        protected Enemy Enemy;
        protected EnemyStateMachine EnemyStateMachine;
        protected Animator AnimatorController;
        protected string AnimationName;

        protected bool isExitingState;
        protected bool isAnimationFinished;
        protected float startTime;

        protected EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine, Animator animatorController, string animationName){
            Enemy = enemy;
            EnemyStateMachine = enemyStateMachine;
            AnimatorController = animatorController;
            AnimationName = animationName;
        }

        public virtual void Enter(){
            isAnimationFinished = false;
            isExitingState = false;
            startTime = Time.time;
            AnimatorController.Play(AnimationName);
        }
        public virtual void Exit(){
            isExitingState = true;
            if(!isAnimationFinished) isAnimationFinished = true;
            AnimatorController.Play(AnimationName);
        }
        public virtual void LogicUpdate(){
            TransitionChecks();
        }
        public virtual void PhysicsUpdate(){
            TransitionChecks();
        }
        public virtual void TransitionChecks(){}
        public virtual void AnimationTrigger(){
            isAnimationFinished = true;
        }
    }
}