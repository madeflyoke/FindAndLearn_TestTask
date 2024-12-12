using System;
using Gameplay.Data;
using UnityEngine;

namespace Gameplay.Views
{
    public class CategoryItemCellView : MonoBehaviour
    {
        public event Action<CategoryItemData> Clicked;

        public Vector2 CellSize => _spriteRenderer.bounds.size;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private CategoryItemView _currentItemView;
        
        public void SetRelatedItemView(CategoryItemView itemView)
        {
            _currentItemView = itemView;
        }
        
        private void OnMouseDown()
        {
            Debug.LogWarning("Clicked ");
            Clicked?.Invoke(_currentItemView.RelatedData);
        }
    }
}