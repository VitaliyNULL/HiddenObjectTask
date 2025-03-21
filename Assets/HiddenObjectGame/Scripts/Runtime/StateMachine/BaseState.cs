namespace HiddenObjectGame.Runtime.StateMachine
{
    public abstract class BaseState : IState
    {
        protected IStateMachine StateMachine;

        protected BaseState(IStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract void Enter();

        public virtual void FixedTick()
        {
            
        }

        public virtual void Tick()
        {
            
        }

        public abstract void Exit();
    }
}