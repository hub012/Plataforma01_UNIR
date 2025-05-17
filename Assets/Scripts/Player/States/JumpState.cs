using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState
{
    float timer;
    float timerLimit = 0.3f;
    private bool hasJumped = false;
    public JumpState(Player player, PlayerStateMachine playerStateMachine, Animator animatorController)
    : base(player, playerStateMachine, animatorController, "Jumping")
    {
    }

    public override void Enter()
    {
        base.Enter();
        timer = 0;
        hasJumped = false;
        player.IsAirborne = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timer += Time.deltaTime;

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!hasJumped)
        {
            hasJumped = true;
            player.playerRigidbody.velocity = new Vector2(player.playerRigidbody.velocity.x, 0);
            player.playerRigidbody.AddForce(Vector2.up * player.jumpSpeed, ForceMode2D.Impulse);

        }

    }

    public override void TransitionChecks()
    {
        base.TransitionChecks();

        if (timer >= timerLimit && player.playerRigidbody.velocity.y <= 0f)
        {
            if (player.IsGrounded())
            {
                player.IsAirborne = false;
                playerStateMachine.ChangeState(player.idleState);
            }
        }


    }

}
