using System.Collections.Generic;
using Gameplay.Data;
using Gameplay.Views;
using UnityEngine;

namespace Gameplay.Spawners
{
    public class CategoryItemsSpawner : MonoBehaviour
    {
        [SerializeField] private CategoryItemView _itemViewPrefab;

        public List<CategoryItemView> SpawnItems(List<CategoryItemData> itemsData, List<Transform> parents,
            Vector2 parentScaleToFit = default)
        {
            var result = new List<CategoryItemView>();
            for (var i = 0; i < parents.Count; i++)
            {
                var parent = parents[i];
                result.Add(SpawnInternal(itemsData[i], parent, parentScaleToFit));
            }

            return result;
        }

        private CategoryItemView SpawnInternal(CategoryItemData itemData, Transform parent,
            Vector2 parentScaleToFit = default)
        {
            var itemView = Instantiate(_itemViewPrefab, parent.transform);
            itemView.Initialize(itemData);
            itemView.SpriteRendererHolder.transform.localRotation = Quaternion.Euler(0,0, -1*itemData.RelatedSpriteData.CustomRotationAngle);
            
            if (parentScaleToFit != default)
            {
                var scalableWrapper = parent.transform.GetChild(0);
                if (scalableWrapper == null)
                {
                    scalableWrapper = new GameObject().transform;
                    scalableWrapper.SetParent(parent,false);
                }

                itemView.transform.SetParent(scalableWrapper.transform,false);
                scalableWrapper.transform.localScale =
                    GetSpriteScaleToFitParent(parentScaleToFit, itemView.SpriteRendererHolder.bounds.size);
            }
            
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