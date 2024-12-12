using System;
using Gameplay.Enums;
using UnityEngine;

namespace Gameplay.Levels.Data
{
    [Serializable]
    public class LevelData
    {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public CategoryType Category { get; private set; }
        [field: SerializeField] public Vector2Int GridSize { get; private set; }
    }
}
