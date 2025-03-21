namespace HiddenObjectGame.Runtime.StateMachine
{
    public interface IStateMachine
    {
        public void InitializeStateMachine();
        public void AddState<T>(T state) where T: IState;
        public void Tick();
        public void FixedTick();
        public void ChangeState<T>() where T : IState;
    }
}