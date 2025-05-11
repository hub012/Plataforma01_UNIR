using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
   public PlayerState _CurrentState{ get; private set;}


   public void ChangeState(PlayerState newState){
      _CurrentState.Exit();
      Debug.Log(newState);
      _CurrentState = newState; 
      _CurrentState.Enter();
   }

   public void initStateMachine(PlayerState initalState){
      _CurrentState = initalState; 
      _CurrentState.Enter();
   }
}
