using UnityEngine;

public class State 
{
    protected global::Player.Player player;
    protected StateMachine StateMachine;
    protected Animator animatorController;
    protected string animationName;

    protected bool isExitingState;
    protected bool isAnimationFinished;
    protected float startTime;
   
    public State(global::Player.Player _player, StateMachine stateMachine, Animator _animatorController, string _animationName){
        player = _player;
        StateMachine = stateMachine;
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