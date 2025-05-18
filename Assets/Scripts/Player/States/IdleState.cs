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
            player.IsAirborne = false;
        
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

            if (player.PlayerControls.inputMove != Vector2.zero)
            {
                PlayerStateMachine.ChangeState(player.walkState);
            }
            if(player.PlayerControls.IsSprinting)
                PlayerStateMachine.ChangeState(player.runState);
                            
            if (player.PlayerControls.IsVerticalAttacking)
                PlayerStateMachine.ChangeState(player.verticalAttackState);
            
            
        
        }
    
    }
}
