using Gameplay.Creators.Interfaces;
using Gameplay.Managers.Interfaces;
using StateMachine.Interfaces;
using UI.Gameplay;
using UI.Interfaces;
using UnityEngine;
using VContainer;

namespace StateMachine
{
    public class EndGameplayState : IState
    {
        private readonly IScreenController _screenController;
        private readonly IFadeController _fadeController;
        private readonly IStateMachine _stateMachine;
        
        private EndGamePopup _endGamePopup;

        public EndGameplayState(IStateMachine stateMachine, IObjectResolver resolver)
        {
            _screenController = resolver.Resolve<IScreenController>();
            _fadeController =  resolver.Resolve<IFadeController>();
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _endGamePopup = _screenController.GetScreen<GameplayScreen>().EndGamePopup;
            _endGamePopup.RestartRequired += RestartLevels;
            _endGamePopup.Show();
        }

        private void RestartLevels()
        {
            _fadeController.ShowFade(onComplete: () =>
            {
                _fadeController.HideFade();
                _stateMachine.ChangeState<InitialLevelStartState>();
            });
        }

        public void Exit()
        {
            _endGamePopup.RestartRequired -= RestartLevels;
        }
    }
}
