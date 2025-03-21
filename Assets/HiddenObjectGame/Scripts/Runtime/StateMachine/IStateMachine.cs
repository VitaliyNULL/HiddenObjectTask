namespace HiddenObjectGame.Runtime.StateMachine
{
    public interface IStateMachine
    {
        public void InitializeStateMachine();
        public void AddState(IState state);
        public void Tick();
        public void FixedTick();
        public void ChangeState<T>() where T : IState;
    }
}