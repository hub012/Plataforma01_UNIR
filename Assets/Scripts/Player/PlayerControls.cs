using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerControls : MonoBehaviour
    {
        private PlayerInput playerInput;

        [Header("Input States")]
        public Vector2 inputMove;
        public bool JumpPressed { get; private set; }
        public bool IsSprinting { get; private set; }
        public bool IsVerticalAttacking { get; private set; }

        void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            if (playerInput != null)
            {
                playerInput.actions["Move"].performed += OnMove;
                playerInput.actions["Move"].canceled += OnMove;

                playerInput.actions["Sprint"].performed += OnSprint;
                playerInput.actions["Sprint"].canceled += OnSprint;

                playerInput.actions["Jump"].performed += OnJump;

                playerInput.actions["VerticalAttack"].performed += OnVerticalAttack;
                playerInput.actions["VerticalAttack"].canceled += OnVerticalAttack;
            }
        }

        private void OnDisable()
        {
            if (playerInput != null)
            {
                playerInput.actions["Move"].performed -= OnMove;
                playerInput.actions["Move"].canceled -= OnMove;

                playerInput.actions["Sprint"].performed -= OnSprint;
                playerInput.actions["Sprint"].canceled -= OnSprint;

                playerInput.actions["Jump"].performed -= OnJump;

                playerInput.actions["VerticalAttack"].performed -= OnVerticalAttack;
                playerInput.actions["VerticalAttack"].canceled -= OnVerticalAttack;
            }
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
        }

        private void OnSprint(InputAction.CallbackContext context)
        {
            IsSprinting = context.performed;
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                JumpPressed = true;
            }
        }

        private void OnVerticalAttack(InputAction.CallbackContext context)
        {
            IsVerticalAttacking = context.performed;
        }

        public void ResetJump()
        {
            JumpPressed = false;
        }

        // Helper properties for cleaner state checks
        public bool HasMovementInput => Mathf.Abs(inputMove.x) > 0.01f;
        public bool IsMovingRight => inputMove.x > 0.01f;
        public bool IsMovingLeft => inputMove.x < -0.01f;
    }
}