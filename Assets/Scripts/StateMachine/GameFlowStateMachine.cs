using System;
using System.Collections.Generic;
using StateMachine.Interfaces;
using UnityEngine;
using VContainer;

namespace StateMachine
{
    public class GameFlowStateMachine : MonoBehaviour, IStateMachine
    {
        private IState _currentState;
        private Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        
        private Func<List<IState>> _factory;
        
        [Inject]
        public void Initialize(Func<List<IState>> statesFactory)
        {
            _factory = statesFactory;
            foreach (IState state in _factory())
            {
                _states.Add(state.GetType(),state);
            }
        }

        public void Start()
        {
            ChangeState<InitialLevelStartState>();
        }

        public void ChangeState<T>() where T: IState
        {
            _currentState?.Exit();
            _currentState = _states[typeof(T)];
            _currentState.Enter();
        }

        private void OnDestroy()
        {
            foreach (var kvp in _states)
            {
                if (kvp.Value is IDisposable disposable)
                {
                    disposable?.Dispose();
                }
            }
        }
    }
}
