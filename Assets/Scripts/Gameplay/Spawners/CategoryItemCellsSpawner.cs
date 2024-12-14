using System;
using System.Collections.Generic;
using Gameplay.Views;
using StateMachine;
using UnityEngine;
using VContainer;

namespace Gameplay.Spawners
{
    public class CategoryItemCellsSpawner : MonoBehaviour
    {
        public Vector2 CellSize => _cellPrefab.CellSize;
    
        [SerializeField] private CategoryItemCellView _cellPrefab;
        
        private Func<CategoryItemCellView, Vector3, Quaternion, Transform, CategoryItemCellView> _factory;
        
        [Inject]
        public void Initialize(Func<CategoryItemCellView, Vector3, Quaternion,Transform, CategoryItemCellView> factory)
        {
            _factory = factory;
        }
        
        public CategoryItemCellView Spawn(Vector2 position, Transform parent)
        {
            return _factory(_cellPrefab,position, Quaternion.identity,  parent);
        }
    
    }
}
