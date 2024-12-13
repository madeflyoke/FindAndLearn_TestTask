using System;
using Gameplay.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Gameplay.Spawners
{
    public class CategoryItemCellsSpawner : MonoBehaviour
    {
        public Vector2 CellSize => _cellPrefab.CellSize;
    
        [SerializeField] private CategoryItemCellView _cellPrefab;
        private IObjectResolver _resolver;

        private void Awake()
        {
            _resolver = FindObjectOfType<LifetimeScope>().Container;
        }

        public CategoryItemCellView Spawn(Vector2 position, Transform parent)
        {
            return _resolver.Instantiate(_cellPrefab, position, Quaternion.identity, parent:parent);
        }
    
    }
}
