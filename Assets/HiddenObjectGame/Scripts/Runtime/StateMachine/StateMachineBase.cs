using System;
using System.Collections.Generic;

namespace HiddenObjectGame.Runtime.StateMachine
{
    public abstract class StateMachineBase : IStateMachine
    {
        private readonly Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        private IState _currentState;
        public abstract void InitializeStateMachine();

        public void AddState(IState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void Tick()
        {
            _currentState.Tick();
        }

        public void FixedTick()
        {
            _currentState.FixedTick();
        }


        public void ChangeState<T>() where T : IState
        {
            if (_states.TryGetValue(typeof(T), out var state))
            {
                _currentState?.Exit();
                _currentState = state;
                _currentState.Enter();
            }
            else
            {
                throw new Exception($"State of type {typeof(T)} not found in the state machine");
            }
        }
    }
}