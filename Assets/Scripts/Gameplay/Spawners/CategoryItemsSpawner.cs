using System.Collections.Generic;
using Configs;
using Gameplay.Data;
using Gameplay.Enums;
using Gameplay.Views;
using UnityEngine;

namespace Gameplay.Spawners
{
    public class CategoryItemsSpawner : MonoBehaviour
    {
        [SerializeField] private CategoriesContainer _categoriesContainer;
        [SerializeField] private CategoryItemView _itemViewPrefab;
        
        public List<CategoryItemView> SpawnRandomMany(CategoryType categoryType, List<Transform> parents, Vector2 parentScaleToFit = default)
        {
            var result = new List<CategoryItemView>();
            var itemsData = _categoriesContainer.GetConfig(categoryType).GetRandomItemsData(parents.Count);
            for (var i = 0; i < parents.Count; i++)
            {
                var parent = parents[i];
                result.Add(SpawnRandomInternal(itemsData[i], parent, parentScaleToFit));
            }

            return result;
        }
        
        public CategoryItemView SpawnRandom(CategoryType categoryType, Transform parent, Vector2 parentScaleToFit = default)
        {
            var categoryConfig = _categoriesContainer.GetConfig(categoryType);

            var itemData= categoryConfig.GetRandomItemData();
            return SpawnRandomInternal(itemData, parent, parentScaleToFit);
        }

        private CategoryItemView SpawnRandomInternal(CategoryItemData itemData, Transform parent, Vector2 parentScaleToFit = default)
        {
            var itemView = Instantiate(_itemViewPrefab, parent.transform);

            if (parentScaleToFit!=default)
            {
                var scalableWrapper = parent.transform.GetChild(0);
                if (scalableWrapper==null)
                {
                    scalableWrapper = new GameObject().transform;
                    scalableWrapper.SetParent(parent);
                    scalableWrapper.localPosition =Vector3.zero;
                }
                
                itemView.transform.SetParent(scalableWrapper.transform);
                itemView.transform.localPosition = Vector3.zero;
                scalableWrapper.transform.localScale =
                    GetSpriteScaleToFitParent(parentScaleToFit, itemData.RelatedSprite.bounds.size);
            }
            
            itemView.Initialize(itemData);
            return itemView;
        }
        
        
        private Vector3 GetSpriteScaleToFitParent(Vector2 parentBoundsSize, Vector2 childBoundsSize)
        {
            var ratioX = parentBoundsSize.x / childBoundsSize.x;
            var ratioY = parentBoundsSize.y / childBoundsSize.y;

            return Vector3.one* Mathf.Min(ratioX, ratioY) * .65f; //TODO Visual settings config?
        }
      
    }
}