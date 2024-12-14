using System;
using System.Threading;
using System.Threading.Tasks;
using Gameplay.Managers.Interfaces;
using StateMachine.Interfaces;
using UI.Gameplay;
using UI.Interfaces;
using VContainer;

namespace StateMachine
{
    public class GameplayState : IState, IDisposable
    {
        private readonly IStateMachine _stateMachine;
        private readonly IAnswersLogicValidator _answersLogicValidator;
        private readonly IScreenController _screenController;
        private CancellationTokenSource _cts;
        
        public GameplayState(IStateMachine stateMachine, IObjectResolver objectResolver)
        {
            _stateMachine = stateMachine;
            _answersLogicValidator = objectResolver.Resolve<IAnswersLogicValidator>();
            _screenController = objectResolver.Resolve<IScreenController>();
        }
        
        public void Enter()
        {
            _answersLogicValidator.CorrectAnswerDone += CompleteLevel;
        }

        private async void CompleteLevel()
        {
            _screenController.GetScreen<GameplayScreen>().TargetView.Hide();
            _cts = new CancellationTokenSource();
            await Task.Delay(TimeSpan.FromSeconds(2), _cts.Token); //TODO Delay config
            _stateMachine.ChangeState<LevelStartState>();
        }

        public void Exit()
        {
            _answersLogicValidator.CorrectAnswerDone -= CompleteLevel;
        }

        public void Dispose()
        {
            _cts?.Dispose();
        }
    }
}
