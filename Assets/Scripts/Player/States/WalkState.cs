using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WalkState : PlayerState
{
    
    public WalkState(Player player, PlayerStateMachine playerStateMachine, Animator animatorController)
    : base(player, playerStateMachine, animatorController, "Walking") 
    {   
    }

    public override void Enter()
    {
        base.Enter(); 
       
        
        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        var playerVelocity = new Vector2(player.PlayerControls.inputMove.x * player.speed, player.playerRigidbody.velocity.y);
        player.playerRigidbody.velocity = playerVelocity;
        
      
    }

    public override void TransitionChecks(){
        base.TransitionChecks();
  
        if(player.PlayerControls.inputMove== Vector2.zero) 
            playerStateMachine.ChangeState(player.idleState);

        if(player.PlayerControls.IsJumping)
             playerStateMachine.ChangeState(player.jumpState);
    
    }
}