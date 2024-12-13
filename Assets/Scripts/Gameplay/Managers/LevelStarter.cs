using System;
using Configs;
using Gameplay.Levels.Data;
using Gameplay.Managers.Interfaces;
using UnityEngine;
using VContainer;

namespace Gameplay.Managers
{
    public class LevelStarter : MonoBehaviour, ILevelStarter
    {
        public event Action<LevelData, bool> LevelStarted;
        
        [SerializeField] private LevelsConfig _levelsConfig;
        private LevelData _currentLevelData;
        private IAnswersLogicValidator _answersValidator;
        
        [Inject]
        public void Construct(IAnswersLogicValidator answersValidator)
        {
            _answersValidator = answersValidator;
        }

        private void OnEnable()
        {
            _answersValidator.CorrectAnswerDone += OnLevelCompleted;
        }

        private void OnLevelCompleted()
        {
            Invoke(nameof(SetNextLevel), _levelsConfig.NextLevelDelay);
        }
        
        private void Start()
        {
            _currentLevelData = _levelsConfig.GetLevelData(0);
            LevelStarted?.Invoke(_currentLevelData, true);
        }

        private void SetNextLevel()
        {
            _currentLevelData = _levelsConfig.GetLevelData(_currentLevelData.Id + 1);
            if (_currentLevelData==null)
            {
                Debug.LogWarning("Out of levels, restarting....");
                _currentLevelData = _levelsConfig.GetLevelData(0);
               
            }
            LevelStarted?.Invoke(_currentLevelData,false);
        }
        
        private void OnDisable()
        {
            _answersValidator.CorrectAnswerDone -= OnLevelCompleted;
            CancelInvoke();
        }
    }
}
