using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Vector2 inputMove;
    Rigidbody2D playerRigidbody;
    [SerializeReference] float speed;
    Animator playerAnimator;

    bool isSprinting;
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Detectar si el jugador tiene la mas minima velocidad
        bool playerHasMovementSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        Walk();
        FlipSprite(playerHasMovementSpeed);
        
        if (!playerHasMovementSpeed)
            Idle();
    }

    void OnMove(InputValue input){

        inputMove = input.Get<Vector2>();
           
    }
    void OnJump(){
        
        Debug.Log("Saltando");
    }

    void OnAttack(){
        
        Debug.Log("Attacking");
    }

    void Walk(){

        var playerVelocity = new Vector2(inputMove.x * speed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;
        playerAnimator.SetBool("IsWalking",true);
    }

    void OnSprint(){
        Debug.Log("Sprinting");
    }

    void Idle(){
        playerAnimator.SetBool("IsWalking",false);
        playerAnimator.SetBool("IsRunning",false);
    }

    /// <summary>
    /// Method <c>FlipSprite</c> Hay un truco para hacer flip del player con cambiar el scale entre -1 y 1 en el eje x
    /// Uso Mathf.Sign para solo sacar si el valor positivo o no y usarlo en base al 1
    /// Si es 0 va hacia el valor positivo es decir uno.
    /// Ejemplos: Mathf.Sign(35) = 1, Mathf.Sign(-40) = -1, Mathf.Sign(0) = 1
    /// </summary>
    void FlipSprite(bool playerHasMovementSpeed){
        if(playerHasMovementSpeed)
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
    } 
}