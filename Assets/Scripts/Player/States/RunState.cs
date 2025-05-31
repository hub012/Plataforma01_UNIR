using UnityEngine;

namespace Player.States
{
    public class RunState : PlayerState
    {
        public RunState(Player player, PlayerStateMachine playerStateMachine, Animator animatorController) 
            : base(player, playerStateMachine, animatorController, "Running")
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // Apply running movement
            var playerVelocity = new Vector2(
                player.PlayerControls.inputMove.x * player.SprintingSpeed, 
                player.playerRigidbody.velocity.y
            );
            player.playerRigidbody.velocity = playerVelocity;
        }

        public override void TransitionChecks()
        {
            base.TransitionChecks();
            
            // Skip transitions if airborne (let jump state handle landing)
            if (player.IsAirborne)
                return;

            // Priority 1: Jump (if grounded and jump pressed)
            if (player.IsGrounded() && player.PlayerControls.JumpPressed)
            {
                player.PlayerControls.ResetJump();
                PlayerStateMachine.ChangeState(player.jumpState);
                return;
            }

            // Priority 2: Vertical Attack
            if (player.PlayerControls.IsVerticalAttacking)
            {
                PlayerStateMachine.ChangeState(player.verticalAttackState);
                return;
            }

            // Priority 3: Stop moving (return to idle)
            if (player.PlayerControls.inputMove == Vector2.zero)
            {
                PlayerStateMachine.ChangeState(player.idleState);
                return;
            }

            // Priority 4: Stop sprinting (switch to walk)
            if (!player.PlayerControls.IsSprinting)
            {
                PlayerStateMachine.ChangeState(player.walkState);
                return;
            }
        }
    }
}