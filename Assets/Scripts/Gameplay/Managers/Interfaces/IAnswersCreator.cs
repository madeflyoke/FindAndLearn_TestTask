using System.Collections.Generic;
using Gameplay.Data;
using Gameplay.Levels.Data;

namespace Gameplay.Managers.Interfaces
{
    public interface IAnswersCreator
    {
        public List<CategoryItemData> Create(LevelData levelData, out CategoryItemData correctAnswer);
    }
}
