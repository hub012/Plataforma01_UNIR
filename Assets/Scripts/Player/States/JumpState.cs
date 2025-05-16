using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(Player player, PlayerStateMachine playerStateMachine, Animator animatorController) 
    : base(player, playerStateMachine, animatorController, "Jumping")
    {
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate(); 
      
        player.playerRigidbody.velocity += new Vector2 (0f, 0.1f);
      
    }

    public override void TransitionChecks()
    {
        base.TransitionChecks();

        if(!player.PlayerControls.IsJumping){
            if(player.PlayerControls.inputMove== Vector2.zero)
                playerStateMachine.ChangeState(player.idleState);

            if(player.PlayerControls.inputMove!= Vector2.zero)
                playerStateMachine.ChangeState(player.walkState);
        }
    }

}
