using System.Collections.Generic;
using System.Linq;
using Gameplay.Enums;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CategoriesContainer", menuName = "Configs/CategoriesContainer")]
    public class CategoriesContainer : ScriptableObject
    {
        [SerializeField] private List<CategoryConfig> _configs;

        public CategoryConfig GetConfig(CategoryType type)
        {
            return _configs.FirstOrDefault(x => x.Type == type);
        }
    }
}
