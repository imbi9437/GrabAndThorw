namespace _Project.Script.Behaviour.StateMachine.Native
{
    public abstract class State<T> : IState<T>
    {
        public virtual void Enter(T owner) { }
        public virtual void Update(T owner) { }
        public virtual void Exit(T owner) { }
    }
}
