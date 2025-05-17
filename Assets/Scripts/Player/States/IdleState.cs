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
        player.IsAirborne = false;
        
    }
    public override void TransitionChecks()
    {
        base.TransitionChecks();

        if (player.IsAirborne)
            return;

           if(player.IsGrounded() && player.PlayerControls.JumpPressed)
        {
            playerStateMachine.ChangeState(player.jumpState);
            player.PlayerControls.ResetJump();
            return;
        }

        if (player.PlayerControls.inputMove != Vector2.zero)
        {
            playerStateMachine.ChangeState(player.walkState);
        }
        if(player.PlayerControls.IsSprinting)
            playerStateMachine.ChangeState(player.runState);
                            
        if (player.PlayerControls.IsVerticalAttacking)
            playerStateMachine.ChangeState(player.verticalAttackState);
            
            
        
    }
    
}
