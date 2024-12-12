using Gameplay.Data;
using UnityEngine;

namespace Gameplay.Views
{
    public class CategoryItemView : MonoBehaviour
    {
        public CategoryItemData RelatedData { get; private set; }
        
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void Initialize(CategoryItemData relatedData)
        {
            RelatedData = relatedData;
            _spriteRenderer.sprite = relatedData.RelatedSprite;
        }
    }
}
