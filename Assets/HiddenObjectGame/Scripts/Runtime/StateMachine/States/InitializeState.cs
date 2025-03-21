using Zenject;

namespace HiddenObjectGame.Runtime.StateMachine.States
{
    public class InitializeState : BaseState
    {
        private readonly SignalBus _signalBus;

        public InitializeState(IStateMachine stateMachine, SignalBus signalBus) : base(stateMachine)
        {
            _signalBus = signalBus;
        }

        public override void Enter()
        {
            _signalBus.Fire(new ChangeStateSignal(this));
        }

        public override void Exit()
        {
        }
    }
}