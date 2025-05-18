using Enemy.States;

namespace Enemy
{
    public class EnemyStateMachine
    {
        public EnemyState CurrentState{ get; private set;}
        public EnemyState PreviousState{ get; private set;}


        public void ChangeState(EnemyState newState)
        {
            PreviousState = CurrentState;
            CurrentState.Exit();
            CurrentState = newState; 
            CurrentState.Enter();
        }

        public void InitStateMachine(EnemyState initalState){
            CurrentState =PreviousState = initalState; 
            CurrentState.Enter();
        }
    }
}