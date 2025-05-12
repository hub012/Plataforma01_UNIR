using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction sprintAction;
    private InputAction jumpAction;
    public bool IsSprinting{get; private set;}
    public bool IsJumping{get; private set;}

    public Vector2 inputMove; // se va
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        sprintAction = playerInput.actions["Sprint"];
        jumpAction = playerInput.actions["Jump"];
        
        // Subscribe to performed/canceled manually
        sprintAction.performed += OnSprintPerformed;
        sprintAction.canceled += OnSprintCanceled;

        jumpAction.performed += OnJumpPerformed;
        jumpAction.canceled += OnJumpCanceled;
    }
    void Start()
    {
       
    }
    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
        IsSprinting = true;
    }

    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        IsSprinting = false;
    }

     private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        IsJumping = true;
        Debug.Log("Jump pressed");
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        IsJumping = false;
        Debug.Log("Jump release");
    }


    void OnMove(InputValue input){ // se va

        inputMove = input.Get<Vector2>();
      
    }
    void OnAttack(){ // se va
        
        Debug.Log("Attacking");
    }


}
