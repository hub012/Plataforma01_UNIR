using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    public Rigidbody2D playerRigidbody;
    [SerializeReference] public float speed;
    [SerializeReference] public float jumpSpeed;
    public float SprintingSpeed{get; private set;}
    public Animator playerAnimator;

    public bool isSprinting;
    public bool isWalking;

    private PlayerStateMachine stateMachine;
    private PlayerState currentState;
    
    #region  Player States
        public WalkState walkState;
        public IdleState idleState;
        public RunState runState;
        public JumpState jumpState;
    #endregion
      #region Player Controls
        public PlayerControls PlayerControls{ get; private set;}
    #endregion

    void Start()
    {
        // Inputs
        PlayerControls = GetComponent<PlayerControls>(); 

        //speed init
        SprintingSpeed = speed * 2;
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        stateMachine = new PlayerStateMachine();

        // Set IdleState as the initial state
        currentState = idleState = new IdleState(this, stateMachine, playerAnimator);
        walkState = new WalkState(this, stateMachine, playerAnimator);
        runState = new RunState(this, stateMachine, playerAnimator);
        jumpState = new JumpState(this, stateMachine, playerAnimator);
        stateMachine.initStateMachine(currentState); //Inicio la maquina con todos los estados predefenidos
    
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine?._CurrentState?.LogicUpdate();
        // Detectar si el jugador tiene la mas minima velocidad y si la ultima velocidad que tuvo fue negativa o positiva
        //bool playerHasMovementSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        
        FlipSprite();

        
        //if (!playerHasMovementSpeed)
    }

    /// <summary>
    /// Method <c>FlipSprite</c> Hay un truco para hacer flip del player con cambiar el scale entre -1 y 1 en el eje x
    /// Uso Mathf.Sign para solo sacar si el valor positivo o no y usarlo en base al 1
    /// Si es 0 va hacia el valor positivo es decir uno.
    /// Ejemplos: Mathf.Sign(35) = 1, Mathf.Sign(-40) = -1, Mathf.Sign(0) = 1
    /// </summary>
    void FlipSprite(){
        if (Mathf.Abs(PlayerControls.inputMove.x) > 0.01f)
        {
            float direction = Mathf.Sign(PlayerControls.inputMove.x); 
            transform.localScale = new Vector2(direction, 1f);
        }
    } 
}