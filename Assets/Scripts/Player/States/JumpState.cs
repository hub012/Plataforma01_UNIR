using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(PlayerController _player, PlayerStateMachine _playerStateMachine, Animator _animatorController) 
    : base(_player, _playerStateMachine, _animatorController, "Jumping")
    {
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate(); 
      
        
      
    }

    public override void TransitionChecks()
    {
        base.TransitionChecks();

        if(!player.PlayerControls.IsJumping){
            if(player.inputMove == Vector2.zero)
                playerStateMachine.ChangeState(player.idleState);

            if(player.inputMove != Vector2.zero)
                playerStateMachine.ChangeState(player.walkState);
        }
    }

}
