using System;
using UnityEngine;

namespace Gameplay.Data
{
    [Serializable]
    public class SpriteData
    {
        [field: SerializeField] public Sprite RelatedSprite { get; private set; }
        [field: SerializeField] public float CustomRotationAngle { get; private set; } //see what you've done
    }
}
