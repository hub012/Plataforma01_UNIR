using System;
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
    public bool JumpPressed{get; private set;}

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

        playerInput.actions["Jump"].performed += ctx => JumpPressed = true;

        playerInput.actions["VerticalAttack"].performed +=  OnVerticalAttack;
        playerInput.actions["VerticalAttack"].canceled +=  OnVerticalAttack;
        

    }
      private void OnDisable()
    {
        playerInput.actions["Move"].performed -=  OnMove;
        playerInput.actions["Move"].canceled -=  OnMove;

        playerInput.actions["Sprint"].performed -=  OnSprint;
        playerInput.actions["Sprint"].canceled -=  OnSprint;

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
   
   public void ResetJump(){
        JumpPressed = false;
   }


}
