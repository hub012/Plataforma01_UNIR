using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(Player player, PlayerStateMachine playerStateMachine, Animator animatorController)
    : base(player, playerStateMachine, animatorController, "Idling") 
    {
    }

    public override void Enter()
    {
        base.Enter(); 
        
    }
    public override void TransitionChecks()
    {
        base.TransitionChecks();
        if(player.PlayerControls.inputMove != Vector2.zero) {
            playerStateMachine.ChangeState(player.walkState);
        }
        if(player.PlayerControls.IsSprinting)
            playerStateMachine.ChangeState(player.runState);
        
          if(player.PlayerControls.IsJumping){
             playerStateMachine.ChangeState(player.jumpState);
        }
    }
    
}
