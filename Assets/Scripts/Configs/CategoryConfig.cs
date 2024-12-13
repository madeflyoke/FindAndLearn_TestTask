using System.Collections.Generic;
using Gameplay.Data;
using Gameplay.Enums;
using UnityEngine;
using Utils;

namespace Configs
{
    [CreateAssetMenu(fileName = "CategoryConfig", menuName = "Configs/CategoryConfig")]
    public class CategoryConfig : ScriptableObject
    {
        [field: SerializeField] public CategoryType Type { get; private set; }
        [field: SerializeField] public List<CategoryItemData> ItemsData { get; private set; }

        public List<CategoryItemData> GetRandomItemsData(int count)
        {
            if (ItemsData.Count<count)
            {
                Debug.LogWarning("Required items count too large, filling with repeatables...");
                var result = new List<CategoryItemData>();
                for (int i = 0; i < count; i++)
                {
                    result.Add(GetRandomItemData());
                }

                return result;
            }
            
            return ItemsData.ShuffledCopy().GetRange(0, count);
        }
        
        public CategoryItemData GetRandomItemData()
        {
            return ItemsData[Random.Range(0, ItemsData.Count)];
        }
    }
}
