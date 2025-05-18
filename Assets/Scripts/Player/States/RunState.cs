using UnityEngine;

namespace Player.States
{
    public class RunState : State
    {
        public RunState(global::Player.Player player, StateMachine stateMachine, Animator animatorController) 
            : base(player, stateMachine, animatorController, "Running")
        {
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate(); 
            var playerVelocity = new Vector2(player.PlayerControls.inputMove.x * player.SprintingSpeed, player.playerRigidbody.velocity.y);
            player.playerRigidbody.velocity = playerVelocity;
      
        }

        public override void TransitionChecks()
        {
            base.TransitionChecks();
            if (player.IsAirborne)
                return;

            if(player.IsGrounded() &&  player.PlayerControls.JumpPressed)
            {
                StateMachine.ChangeState(player.jumpState);
                player.PlayerControls.ResetJump();
                return;
            }
            if (player.PlayerControls.inputMove == Vector2.zero)
                StateMachine.ChangeState(player.idleState);

            if(!player.PlayerControls.IsSprinting)
                StateMachine.ChangeState(player.walkState);
        
            if(player.PlayerControls.IsJumping){
                StateMachine.ChangeState(player.jumpState);
                player.PlayerControls.ResetJump(); // con esto evito que entre mas de una vez 
            }
        }

    }
}
