using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerState
{
    public RunState(PlayerController _player, PlayerStateMachine _playerStateMachine, Animator _animatorController) 
    : base(_player, _playerStateMachine, _animatorController, "Running")
    {
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate(); 
      
    }

}
