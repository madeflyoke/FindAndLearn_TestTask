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

        #region Container Methods

        public List<CategoryItemData> GetRandomItemsDataExcept(int count, HashSet<CategoryItemData> exclude,
            out CategoryItemData uniqueData)
        {
            var resultHashSet = new HashSet<CategoryItemData>(ItemsData.ShuffledCopy());
            resultHashSet.ExceptWith(exclude);
            
            var countDifference = resultHashSet.Count - count;
            if (countDifference < 0)
            {
                uniqueData = resultHashSet.Count == 0
                    ? ItemsData[Random.Range(0, ItemsData.Count)]
                    : resultHashSet.ToList()[Random.Range(0, resultHashSet.Count)];

                Debug.LogWarning(
                    $"Required items count conditions too large and too strange, filling {Mathf.Abs(countDifference)} with repeatables...");
                return FillItemsWithRepeatables(resultHashSet, Mathf.Abs(countDifference), uniqueData);
            }
            else
            {
                var resultList =resultHashSet.ToList().GetRange(0, count);
                uniqueData = resultList[Random.Range(0, resultList.Count)];
                return resultList;
            }
        }

        private List<CategoryItemData> FillItemsWithRepeatables(HashSet<CategoryItemData> currentItems,
            int requiredCount, CategoryItemData exclude)
        {
            var result = currentItems.ToList();

            if (result.Contains(exclude) == false)
            {
                result.Add(exclude);
                --requiredCount;
            }

            for (int i = 0; i < requiredCount; i++)
            {
                result.Add(GetRandomItemData(exclude));
            }

            return result;
        }

        private CategoryItemData GetRandomItemData(CategoryItemData exclude)
        {
            var correctList = ItemsData.Where(x => x != exclude).ToList();
            return correctList[Random.Range(0, correctList.Count)];
        }

        #endregion
    }
}