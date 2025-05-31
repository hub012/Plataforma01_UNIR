using UnityEngine;

namespace Player.States
{
    public class JumpState : PlayerState
    {
        private float jumpCooldown = 0.1f; // Minimum time before we can check for landing
        private float timer;
        private bool hasJumped = false;
        private bool jumpCut = false; // Track if jump was cut short

        public JumpState(Player player, PlayerStateMachine playerStateMachine, Animator animatorController)
            : base(player, playerStateMachine, animatorController, "Jumping")
        {
        }

        public override void Enter()
        {
            base.Enter();
            timer = 0f;
            hasJumped = false;
            jumpCut = false;
            player.IsAirborne = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            timer += Time.deltaTime;

            // Handle variable jump height (cut jump short if button released)
            if (!jumpCut && !player.PlayerControls.JumpPressed && player.playerRigidbody.velocity.y > player.jumpCutHeight)
            {
                jumpCut = true;
                player.playerRigidbody.velocity = new Vector2(
                    player.playerRigidbody.velocity.x, 
                    player.playerRigidbody.velocity.y * 0.5f // Cut jump height in half
                );
            }

            // Allow horizontal movement while jumping
            if (player.PlayerControls.HasMovementInput)
            {
                float horizontalSpeed = player.PlayerControls.IsSprinting ? player.SprintingSpeed : player.speed;
                
                // Slightly reduced air control for more realistic feel
                float airControlModifier = 0.8f;
                
                player.playerRigidbody.velocity = new Vector2(
                    player.PlayerControls.inputMove.x * horizontalSpeed * airControlModifier, 
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

        public override void Exit()
        {
            base.Exit();
            // Reset gravity scale when exiting jump
            player.playerRigidbody.gravityScale = 1f;
        }
    }
}