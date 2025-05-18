using System;
using Enemy.States;
using UnityEngine;

namespace Enemy
{
    public class Enemy: MonoBehaviour
    {
        // que necesitmaos?
        // Vida
        //vanim
        // rigid vobodyh
        //
        protected Rigidbody2D Rb;
        protected Animator Animator;
        #region Enemy States
            protected IdleState IdleState;
        #endregion
        protected EnemyStateMachine EnemyStateMachine;
        protected EnemyState CurrentState;
        

        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            
            EnemyStateMachine = new EnemyStateMachine();
            CurrentState = IdleState =  new IdleState(this, EnemyStateMachine, Animator);
            EnemyStateMachine.InitStateMachine(CurrentState);
        }
    }
}