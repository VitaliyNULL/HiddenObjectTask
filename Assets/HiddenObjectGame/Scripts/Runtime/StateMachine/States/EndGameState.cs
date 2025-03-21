using Zenject;

namespace HiddenObjectGame.Runtime.StateMachine.States
{
    public class EndGameState : BaseState
    {
        private SignalBus _signalBus;

        public EndGameState(IStateMachine stateMachine, SignalBus signalBus) : base(stateMachine)
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