namespace HiddenObjectGame.Runtime.StateMachine.States
{
    public class ChangeStateSignal
    {
        public IState CurrentState { get; }

        public ChangeStateSignal(IState state)
        {
            CurrentState = state;
        }
    }
}