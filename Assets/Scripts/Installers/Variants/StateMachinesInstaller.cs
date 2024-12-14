using System;
using System.Collections.Generic;
using StateMachine;
using StateMachine.Interfaces;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers.Variants
{
    public class StateMachinesInstaller : ScopeInstaller
    {
        [SerializeField] private GameFlowStateMachine _gameFlowStateMachine;
        
        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterComponent(_gameFlowStateMachine).AsSelf();
            
            builder.RegisterFactory<List<IState>>(container =>
            {
                return () => CreateStates(container);
            }, Lifetime.Scoped);

            
            List<IState> CreateStates(IObjectResolver resolver)
            {
                var result = new List<IState>();

                void AddState<T>() where T:IState
                {
                    result.Add(
                        (IState) Activator.CreateInstance(typeof(T), _gameFlowStateMachine, resolver));
                }
                
                //States here
                AddState<InitialLevelStartState>();
                AddState<LevelStartState>();
                AddState<GameplayState>();
                AddState<EndGameplayState>();
                return result;
            }
        }
    }
}
