using System;
using Configs;
using Gameplay.Levels.Data;
using UnityEngine;

namespace Gameplay.Managers
{
    public class LevelStarter : MonoBehaviour
    {
        public event Action<LevelData> LevelStarted;
        
        [SerializeField] private LevelsConfig _levelsConfig;
        private LevelData _currentLevelData;

        private void Start()
        {
            _currentLevelData = _levelsConfig.GetLevelData(0);
            LevelStarted?.Invoke(_currentLevelData);
        }

        public void SetNextLevel()
        {
            var nextLevelIndex = _currentLevelData.Id + 1;
            var levelData = _levelsConfig.GetLevelData(nextLevelIndex);
            if (levelData==null)
            {
                Debug.LogWarning("Out of levels, restarting....");
                _currentLevelData = _levelsConfig.GetLevelData(0);
                LevelStarted?.Invoke(_currentLevelData);
            }
            else
            {
                _currentLevelData = levelData;
                LevelStarted?.Invoke(_currentLevelData);
            }
        }
    }
}
