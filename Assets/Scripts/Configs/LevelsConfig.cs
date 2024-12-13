using System.Collections.Generic;
using System.Linq;
using Gameplay.Levels.Data;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/LevelsConfig")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private List<LevelData> _levelsData;
        [field: SerializeField] public float NextLevelDelay { get; private set; }

        public LevelData GetLevelData(int id)
        {
            return _levelsData.FirstOrDefault(x => x.Id == id);
        }
    }
}
