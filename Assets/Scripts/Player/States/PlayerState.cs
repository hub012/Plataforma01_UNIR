using UnityEngine;

namespace Player.States
{
    public class PlayerState 
    {
        protected Player player;
        protected PlayerStateMachine PlayerStateMachine;
        protected Animator animatorController;
        protected string animationName;

        protected bool isExitingState;
        protected bool isAnimationFinished;
        protected float startTime;
   
        public PlayerState(Player _player, PlayerStateMachine playerStateMachine, Animator _animatorController, string _animationName){
            player = _player;
            PlayerStateMachine = playerStateMachine;
            animatorController = _animatorController;
            animationName = _animationName;
        }

        public virtual void Enter(){
            isAnimationFinished = false;
            isExitingState = false;
            startTime = Time.time;
            animatorController.Play(animationName);
        }
        public virtual void Exit(){
            isExitingState = true;
            if(!isAnimationFinished) isAnimationFinished = true;
            animatorController.Play(animationName);
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
