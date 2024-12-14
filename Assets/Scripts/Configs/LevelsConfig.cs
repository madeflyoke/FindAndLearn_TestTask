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

        public LevelData GetLevelData(int id)
        {
            return _levelsData.FirstOrDefault(x => x.Id == id);
        }
    }
}
