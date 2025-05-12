using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
   public PlayerState _CurrentState{ get; private set;}
   public PlayerState _PreviousState{ get; private set;}


   public void ChangeState(PlayerState newState)
   {
      _PreviousState = _CurrentState;
      _CurrentState.Exit();
      _CurrentState = newState; 
      _CurrentState.Enter();
   }

   public void initStateMachine(PlayerState initalState){
      _CurrentState =_PreviousState = initalState; 
      _CurrentState.Enter();
   }
}
