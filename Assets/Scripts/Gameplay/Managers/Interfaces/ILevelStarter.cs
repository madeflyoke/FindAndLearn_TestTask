using Gameplay.Levels.Data;

namespace Gameplay.Managers.Interfaces
{
    public interface ILevelStarter
    {
        public bool StartLevel(out LevelData levelData, bool initial);
    }
}
