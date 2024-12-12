using System;
using UnityEngine;

namespace Gameplay.Data
{
    [Serializable]
    public class CategoryItemData
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public Sprite RelatedSprite { get; private set; }
    }
}