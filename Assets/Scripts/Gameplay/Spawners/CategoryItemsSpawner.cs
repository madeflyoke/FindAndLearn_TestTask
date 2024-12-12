using Configs;
using Gameplay.Enums;
using Gameplay.Views;
using UnityEngine;

namespace Gameplay.Spawners
{
    public class CategoryItemsSpawner : MonoBehaviour
    {
        [SerializeField] private CategoriesContainer _categoriesContainer;
        [SerializeField] private CategoryItemView _itemViewPrefab;
        
        public CategoryItemView SpawnRandom(CategoryType categoryType, Transform parent, Vector2 parentScaleToFit = default)
        {
            var categoryConfig = _categoriesContainer.GetConfig(categoryType);
            
            var itemView = Instantiate(_itemViewPrefab, parent.transform);
            var itemData= categoryConfig.GetRandomItemData();
            
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