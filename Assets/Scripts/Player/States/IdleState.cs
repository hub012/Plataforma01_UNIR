using UnityEngine;

namespace Player.States
{
    public class IdleState : PlayerState
    {
        public IdleState(Player player, PlayerStateMachine playerStateMachine, Animator animatorController)
            : base(player, playerStateMachine, animatorController, "Idling") 
        {
        }

        public override void Enter()
        {
            base.Enter();
            // Stop horizontal movement when entering idle
            player.playerRigidbody.velocity = new Vector2(0, player.playerRigidbody.velocity.y);
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

            // Priority 3: Movement (Walk or Run based on sprint input)
            if (player.PlayerControls.HasMovementInput)
            {
                if (player.PlayerControls.IsSprinting)
                {
                    PlayerStateMachine.ChangeState(player.runState);
                }
                else
                {
                    PlayerStateMachine.ChangeState(player.walkState);
                }
                return;
            }
        }
    }
}