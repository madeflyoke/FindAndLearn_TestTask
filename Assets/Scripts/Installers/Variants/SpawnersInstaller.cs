using Gameplay.Spawners;
using Gameplay.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers.Variants
{
    public class SpawnersInstaller : ScopeInstaller
    {
        [SerializeField] private CategoryItemCellsSpawner _itemCellsSpawner;

        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterComponent(_itemCellsSpawner).AsSelf();

            builder.RegisterFactory<CategoryItemCellView, Vector3, Quaternion, Transform, CategoryItemCellView>(
                container => container.Instantiate, Lifetime.Scoped);
        }
    }
}