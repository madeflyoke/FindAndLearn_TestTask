using Gameplay.Creators;
using Gameplay.Creators.Interfaces;
using Gameplay.Managers;
using Gameplay.Managers.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers
{
    public class GameplayManagersInstaller : LifetimeScope
    {
        [SerializeField] private LevelStarter _levelStarter;
        [SerializeField] private GameFieldCreator _gameFieldCreator;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent<ILevelStarter>(_levelStarter).AsSelf();
            builder.RegisterComponent<ICategoryItemsCreator>(_gameFieldCreator).AsSelf();
            builder.RegisterInstance<IAnswersLogicValidator>(new AnswersLogicHandler(_gameFieldCreator)).AsSelf();
        }
    }
}
