using HiddenObjectGame.Runtime.StateMachine.States;
using Zenject;

namespace HiddenObjectGame.Runtime.StateMachine
{
    public class GameStateMachine : StateMachineBase, IInitializable
    {
        private readonly SignalBus _signalBus;

        public GameStateMachine(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public sealed override void InitializeStateMachine()
        {
            AddState(new InitializeState(this, _signalBus));
            AddState(new GameState(this));
            AddState(new EndGameState(this, _signalBus));
        }

        public void Initialize()
        {
            InitializeStateMachine();
            ChangeState<InitializeState>();
        }
    }
}