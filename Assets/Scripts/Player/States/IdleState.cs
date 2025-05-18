using UnityEngine;

namespace Player.States
{
    public class IdleState : PlayerState
    {
        public IdleState(global::Player.Player player, StateMachine stateMachine, Animator animatorController)
            : base(player, stateMachine, animatorController, "Idling") 
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
                StateMachine.ChangeState(player.jumpState);
                player.PlayerControls.ResetJump();
                return;
            }

            if (player.PlayerControls.inputMove != Vector2.zero)
            {
                StateMachine.ChangeState(player.walkState);
            }
            if(player.PlayerControls.IsSprinting)
                StateMachine.ChangeState(player.runState);
                            
            if (player.PlayerControls.IsVerticalAttacking)
                StateMachine.ChangeState(player.verticalAttackState);
            
            
        
        }
    
    }
}
