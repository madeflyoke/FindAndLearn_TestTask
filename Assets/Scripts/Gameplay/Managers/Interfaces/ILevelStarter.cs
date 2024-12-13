using System;
using Gameplay.Levels.Data;

namespace Gameplay.Managers.Interfaces
{
    public interface ILevelStarter
    {
        /// <param name="bool">First level launch</param>
        public event Action<LevelData, bool> LevelStarted;
    }
}
