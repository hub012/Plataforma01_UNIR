using UnityEngine;

namespace Player.States
{
    public class VerticalAttackState  : PlayerState
    {
        public VerticalAttackState(Player player, PlayerStateMachine playerStateMachine, Animator animatorController) 
            : base(player, playerStateMachine, animatorController, "VerticalAttacking")
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void TransitionChecks()
        {
            base.TransitionChecks();
            if (player.IsAirborne)
                return;

            if(player.IsGrounded() && player.PlayerControls.JumpPressed)
            {
                PlayerStateMachine.ChangeState(player.jumpState);
                player.PlayerControls.ResetJump();
                return;
            }
            if (!player.PlayerControls.IsVerticalAttacking)
                PlayerStateMachine.ChangeState(PlayerStateMachine.PreviousState);
               

        }
    }
}
