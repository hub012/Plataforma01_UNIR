using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(Player player, PlayerStateMachine playerStateMachine, Animator animatorController) 
    : base(player, playerStateMachine, animatorController, "Jumping")
    {
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.playerRigidbody.AddForce(Vector2.up * player.jumpSpeed, ForceMode2D.Impulse);
        Debug.Log("entroe");
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
