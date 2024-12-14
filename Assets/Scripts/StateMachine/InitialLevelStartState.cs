using System.Collections.Generic;
using Gameplay.Creators.Interfaces;
using Gameplay.Data;
using Gameplay.Levels.Data;
using Gameplay.Managers.Interfaces;
using StateMachine.Interfaces;
using UI;
using UI.Gameplay;
using UI.Interfaces;
using UnityEngine;
using VContainer;

namespace StateMachine
{
    public class InitialLevelStartState : IState
    {
        private readonly ILevelStarter _levelStarter;
        private readonly IStateMachine _stateMachine;
        private readonly ILevelItemsFieldCreator _fieldCreator;
        private readonly IAnswersCreator _answersCreator;
        private readonly IScreenController _screenController;

        public InitialLevelStartState(IStateMachine stateMachine, IObjectResolver resolver)
        {
            _levelStarter = resolver.Resolve<ILevelStarter>();
            _fieldCreator = resolver.Resolve<ILevelItemsFieldCreator>();
            _answersCreator = resolver.Resolve<IAnswersCreator>();
            _screenController = resolver.Resolve<IScreenController>();
            _stateMachine = stateMachine;
        }
        
        public virtual void Enter()
        {
            bool initial = GetType() == typeof(InitialLevelStartState);
            
            if (_levelStarter.StartLevel(out LevelData levelData, initial)==false)
            {
                _stateMachine.ChangeState<EndGameplayState>();
                return;
            }
            
            var answerVariants =_answersCreator.Create(levelData, out var correctAnswer);
            
            _screenController.ShowScreen<GameplayScreen>();
            _screenController.GetScreen<GameplayScreen>().TargetView.Show(correctAnswer, initial);
            
            _fieldCreator.Create(levelData, answerVariants,initial);
            _stateMachine.ChangeState<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}
