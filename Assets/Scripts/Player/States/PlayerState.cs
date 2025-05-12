using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
   protected PlayerController player;
   protected PlayerStateMachine playerStateMachine;
   protected Animator animatorController;
   protected string animationName;

   protected bool isExitingState;
   protected bool isAnimationFinished;
   protected float startTime;
   
   public PlayerState(PlayerController _player, PlayerStateMachine _playerStateMachine, Animator _animatorController, string _animationName){
    player = _player;
    playerStateMachine = _playerStateMachine;
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
    public virtual void PhysicsUpdate(){}
    public virtual void TransitionChecks(){}
    public virtual void AnimationTrigger(){
     isAnimationFinished = true;
   }
}
