using Gameplay.Views;
using UnityEngine;

namespace Gameplay.Spawners
{
    public class CategoryItemCellsSpawner : MonoBehaviour
    {
        public Vector2 CellSize => _cellPrefab.CellSize;
    
        [SerializeField] private CategoryItemCellView _cellPrefab;

        public CategoryItemCellView Spawn(Vector2 position, Transform parent)
        {
            return Instantiate(_cellPrefab, position, Quaternion.identity, parent:parent);
        }
    
    }
}
