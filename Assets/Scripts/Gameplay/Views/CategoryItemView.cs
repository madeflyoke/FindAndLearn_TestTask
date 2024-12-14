using Gameplay.Data;
using UnityEngine;

namespace Gameplay.Views
{
    public class CategoryItemView : MonoBehaviour
    {
        public CategoryItemData RelatedData { get; private set; }

        [field: SerializeField] public SpriteRenderer SpriteRendererHolder { get; private set; }
        
        public void Initialize(CategoryItemData relatedData)
        {
            RelatedData = relatedData;
            SpriteRendererHolder.sprite = relatedData.RelatedSpriteData.RelatedSprite;
        }
    }
}
