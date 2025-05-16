using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private PlayerInput playerInput;

    #region Controls
    public bool IsSprinting{get; private set;}
    public bool IsJumping{get; private set;}
    public bool IsVerticalAttacking{get; private set;}

    

    #endregion
  
    public Vector2 inputMove; 
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        
    }

    private void OnEnable()
    {
        playerInput.actions["Move"].performed +=  OnMove;
        playerInput.actions["Move"].canceled +=  OnMove;

        playerInput.actions["Sprint"].performed +=  OnSprint;
        playerInput.actions["Sprint"].canceled +=  OnSprint;

        playerInput.actions["Jump"].performed +=  OnJump;
        playerInput.actions["Jump"].canceled +=  OnJump;

        playerInput.actions["VerticalAttack"].performed +=  OnVerticalAttack;
        playerInput.actions["VerticalAttack"].canceled +=  OnVerticalAttack;
        

    }
      private void OnDisable()
    {
        playerInput.actions["Move"].performed -=  OnMove;
        playerInput.actions["Move"].canceled -=  OnMove;

        playerInput.actions["Sprint"].performed -=  OnSprint;
        playerInput.actions["Sprint"].canceled -=  OnSprint;

        playerInput.actions["Jump"].performed -=  OnJump;
        playerInput.actions["Jump"].canceled -=  OnJump;

        playerInput.actions["VerticalAttack"].performed -=  OnVerticalAttack;
        playerInput.actions["VerticalAttack"].canceled -=  OnVerticalAttack;

    }
    private void OnSprint( InputAction.CallbackContext context)
    {
        if(context.performed){
            IsSprinting = true;
        }
        if(context.canceled){
             IsSprinting = false;
        }
    }


    private void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed){
            IsJumping = true;
             Debug.Log("Jump pressed");
        }
        if(context.canceled){
            IsJumping = false;
            Debug.Log("Jump release");
        }
        
    }


    private void OnVerticalAttack(InputAction.CallbackContext context)
    {
        if(context.performed){
            IsVerticalAttacking = true;
        }
        if(context.canceled){
            IsVerticalAttacking = false;
        }
    }

    void OnMove(InputAction.CallbackContext context){ 

        inputMove = context.ReadValue<Vector2>();
      
    }
   


}
