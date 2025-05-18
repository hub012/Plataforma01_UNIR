using System.Collections;
using System.Collections.Generic;
using Player.States;
using UnityEngine;

public class StateMachine 
{
   public State CurrentState{ get; private set;}
   public State PreviousState{ get; private set;}


   public void ChangeState(State newState)
   {
      PreviousState = CurrentState;
      CurrentState.Exit();
      CurrentState = newState; 
      CurrentState.Enter();
   }

   public void InitStateMachine(State initalState){
      CurrentState =PreviousState = initalState; 
      CurrentState.Enter();
   }
}
