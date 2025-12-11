namespace _Project.Script.Behaviour.StateMachine.Native
{
    public class StateMachine<T>
    {
        private T owner;
        private IState<T> prevState;
        private IState<T> currentState;

        public StateMachine(T owner)
        {
            this.owner = owner;
        }
        
        public void Update() => currentState?.Update(owner);
        public void Rollback() => ChangeState(prevState);
        public void ChangeState(IState<T> newState)
        {
            if (currentState == newState) return;
            
            prevState = currentState;
            currentState?.Exit(owner);
            currentState = newState;
            currentState?.Enter(owner);
        }
    }
}
