using Configs;
using Gameplay.Levels.Data;
using Gameplay.Managers.Interfaces;
using UnityEngine;

namespace Gameplay.Managers
{
    public class LevelStarter : MonoBehaviour, ILevelStarter
    {
        [SerializeField] private LevelsConfig _levelsConfig;
        private LevelData _currentLevelData;

        public bool StartLevel(out LevelData levelData, bool initial)
        {
            var isValid = TryStartNextLevel(initial ? 0 : _currentLevelData.Id + 1);
            levelData = _currentLevelData;
            return isValid;
        }
        
        private bool TryStartNextLevel(int id)
        {
            _currentLevelData = _levelsConfig.GetLevelData(id);
            return _currentLevelData != null;
        }
    }
}
