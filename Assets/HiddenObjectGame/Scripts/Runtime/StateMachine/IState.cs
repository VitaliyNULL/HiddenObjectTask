namespace HiddenObjectGame.Runtime.StateMachine
{
    public interface IState
    {
        public void Enter();
        public void FixedTick();
        public void Tick();
        public void Exit();
    }
}