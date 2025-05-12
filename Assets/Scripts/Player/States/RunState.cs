using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerState
{
    public RunState(PlayerController player, PlayerStateMachine playerStateMachine, Animator animatorController) 
    : base(player, playerStateMachine, animatorController, "Running")
    {
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate(); 
        var playerVelocity = new Vector2(player.inputMove.x * player.SprintingSpeed, player.playerRigidbody.velocity.y);
        player.playerRigidbody.velocity = playerVelocity;
      
    }

    public override void TransitionChecks()
    {
        base.TransitionChecks();
        if(player.inputMove == Vector2.zero)
            playerStateMachine.ChangeState(player.idleState);

        if(!player.PlayerControls.IsSprinting)
            playerStateMachine.ChangeState(player.walkState);
        
        if(player.PlayerControls.IsJumping)
             playerStateMachine.ChangeState(player.jumpState);
    }

}
