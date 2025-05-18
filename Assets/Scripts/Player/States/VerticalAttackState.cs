using UnityEngine;

namespace Player.States
{
    public class VerticalAttackState  : PlayerState
    {
        public VerticalAttackState(global::Player.Player player, StateMachine stateMachine, Animator animatorController) 
            : base(player, stateMachine, animatorController, "VerticalAttacking")
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
                StateMachine.ChangeState(player.jumpState);
                player.PlayerControls.ResetJump();
                return;
            }
            if (!player.PlayerControls.IsVerticalAttacking)
                StateMachine.ChangeState(StateMachine.PreviousState);
               

        }
    }
}
