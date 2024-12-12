using System.Collections.Generic;
using Gameplay.Data;
using Gameplay.Enums;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CategoryConfig", menuName = "Configs/CategoryConfig")]
    public class CategoryConfig : ScriptableObject
    {
        [field: SerializeField] public CategoryType Type { get; private set; }
        [field: SerializeField] public List<CategoryItemData> ItemsData { get; private set; }

        public CategoryItemData GetRandomItemData()
        {
            return ItemsData[Random.Range(0, ItemsData.Count)];
        }
    }
}
