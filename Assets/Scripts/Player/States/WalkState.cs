using UnityEngine;

namespace Player.States
{
    public class WalkState : PlayerState
    {
        public WalkState(Player player, PlayerStateMachine playerStateMachine, Animator animatorController)
            : base(player, playerStateMachine, animatorController, "Walking") 
        {   
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // Apply horizontal movement
            Vector2 velocity = new Vector2(
                player.PlayerControls.inputMove.x * player.speed, 
                player.playerRigidbody.velocity.y
            );
            player.playerRigidbody.velocity = velocity;
        }

        public override void TransitionChecks()
        {
            base.TransitionChecks();

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

            // Priority 3: Sprint (if still moving and sprint pressed)
            if (player.PlayerControls.HasMovementInput && player.PlayerControls.IsSprinting)
            {
                PlayerStateMachine.ChangeState(player.runState);
                return;
            }

            // Priority 4: Stop moving (return to idle)
            if (!player.PlayerControls.HasMovementInput)
            {
                PlayerStateMachine.ChangeState(player.idleState);
                return;
            }
        }
    }
}