using System.Collections;
using System.Collections.Generic;
using Player.States;
using UnityEngine;

public class StateMachine 
{
   public PlayerState CurrentState{ get; private set;}
   public PlayerState PreviousState{ get; private set;}


   public void ChangeState(PlayerState newState)
   {
      PreviousState = CurrentState;
      CurrentState.Exit();
      CurrentState = newState; 
      CurrentState.Enter();
   }

   public void InitStateMachine(PlayerState initalState){
      CurrentState =PreviousState = initalState; 
      CurrentState.Enter();
   }
}
