using System;
using System.Collections.Generic;
using Gameplay.Data;
using Gameplay.Levels.Data;

namespace Gameplay.Creators.Interfaces
{
    public interface ILevelItemsFieldCreator
    {
        public void Create(LevelData levelData, List<CategoryItemData> itemsData, bool animated);
    }
}
