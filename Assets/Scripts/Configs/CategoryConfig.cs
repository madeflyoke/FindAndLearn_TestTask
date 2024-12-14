using System.Collections.Generic;
using System.Linq;
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

        public List<CategoryItemData> GetRandomItemsDataExcept(int count, HashSet<CategoryItemData> exclude)
        {
            var resultHashSet = new HashSet<CategoryItemData>(ItemsData.ShuffledCopy());
            resultHashSet.ExceptWith(exclude);

            var countDifference = resultHashSet.Count - count;
            if (countDifference < 0)
            {
                Debug.LogWarning(
                    $"Required items count conditions too large and too strange, filling {Mathf.Abs(countDifference)} with repeatables...");
                return FillItemsWithRepeatables(resultHashSet, Mathf.Abs(countDifference));
            }

            return resultHashSet.ToList().GetRange(0, count);
        }

        private List<CategoryItemData> FillItemsWithRepeatables(HashSet<CategoryItemData> currentItems, int requiredCount)
        {
            var result = currentItems.ToList();
            for (int i = 0; i < requiredCount; i++)
            {
                result.Add(GetRandomItemData());
            }
            return result;
        }

        private CategoryItemData GetRandomItemData()
        {
            return ItemsData[Random.Range(0, ItemsData.Count)];
        }
    }
}