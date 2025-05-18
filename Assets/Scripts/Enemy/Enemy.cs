using System;
using Enemy.States;
using UnityEngine;

namespace Enemy
{
    public class Enemy: MonoBehaviour
    {
        protected int Life; //TODO estudiar posibiliad de hacerla property
        protected Rigidbody2D Rb;
        protected Animator Animator;
        #region Enemy States
            protected IdleState IdleState;
            protected PatrolState PatrolState;
        #endregion
        protected EnemyStateMachine EnemyStateMachine;
        protected EnemyState CurrentState;
        

        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            
            EnemyStateMachine = new EnemyStateMachine();
            CurrentState = IdleState =  new IdleState(this, EnemyStateMachine, Animator);
            PatrolState =  new PatrolState(this, EnemyStateMachine, Animator);
            EnemyStateMachine.InitStateMachine(CurrentState);
        }
    }
}