using Gameplay.Creators;
using Gameplay.Managers;
using Gameplay.Managers.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers.Variants
{
    public class GameplayManagersInstaller : ScopeInstaller
    {
        [SerializeField] private LevelStarter _levelStarter;
        [SerializeField] private ItemsFieldCreator _itemsFieldCreator;
        [SerializeField] private AnswersLogicHandler _answersLogicHandler;

        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterComponent<ILevelStarter>(_levelStarter).AsSelf();
            builder.RegisterComponent(_itemsFieldCreator).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(_answersLogicHandler).AsSelf().AsImplementedInterfaces();
        }
    }
}