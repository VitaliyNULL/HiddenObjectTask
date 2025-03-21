using System;
using System.Collections.Generic;
using Zenject;

namespace HiddenObjectGame.Runtime.StateMachine
{
    public abstract class StateMachineBase : IStateMachine
    {
        private readonly Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        private IState _currentState;
        public abstract void InitializeStateMachine();
        
        public void AddState<T>(T state) where T : IState
        {
            _states.Add(typeof(T), state);
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