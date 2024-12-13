using System;
using System.Collections.Generic;
using Gameplay.Data;

namespace Gameplay.Creators.Interfaces
{
    public interface ICategoryItemsCreator
    {
        public event Action<List<CategoryItemData>> ItemsCreated;
    }
}
