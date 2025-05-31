using UnityEngine;

namespace Player.States
{
    public class JumpState : PlayerState
    {
        private float jumpCooldown = 0.1f; // Minimum time before we can check for landing
        private float timer;
        private bool hasJumped = false;

        public JumpState(Player player, PlayerStateMachine playerStateMachine, Animator animatorController)
            : base(player, playerStateMachine, animatorController, "Jumping")
        {
        }

        public override void Enter()
        {
            base.Enter();
            timer = 0f;
            hasJumped = false;
            player.IsAirborne = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            timer += Time.deltaTime;

            // Allow horizontal movement while jumping
            if (player.PlayerControls.HasMovementInput)
            {
                float horizontalSpeed = player.PlayerControls.IsSprinting ? player.SprintingSpeed : player.speed;
                player.playerRigidbody.velocity = new Vector2(
                    player.PlayerControls.inputMove.x * horizontalSpeed, 
                    player.playerRigidbody.velocity.y
                );
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            // Apply jump force once
            if (!hasJumped)
            {
                hasJumped = true;
                player.playerRigidbody.velocity = new Vector2(
                    player.playerRigidbody.velocity.x, 
                    player.jumpSpeed
                );
            }
        }

        public override void TransitionChecks()
        {
            base.TransitionChecks();

            // Only check for landing after cooldown and when falling
            if (timer >= jumpCooldown && player.playerRigidbody.velocity.y <= 0f)
            {
                if (player.IsGrounded())
                {
                    player.IsAirborne = false;
                    
                    // Determine which state to return to based on input
                    if (player.PlayerControls.IsVerticalAttacking)
                    {
                        PlayerStateMachine.ChangeState(player.verticalAttackState);
                    }
                    else if (player.PlayerControls.HasMovementInput)
                    {
                        if (player.PlayerControls.IsSprinting)
                        {
                            PlayerStateMachine.ChangeState(player.runState);
                        }
                        else
                        {
                            PlayerStateMachine.ChangeState(player.walkState);
                        }
                    }
                    else
                    {
                        PlayerStateMachine.ChangeState(player.idleState);
                    }
                }
            }
        }
    }
}