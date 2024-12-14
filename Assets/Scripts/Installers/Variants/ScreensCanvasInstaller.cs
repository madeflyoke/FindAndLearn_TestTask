using UI;
using UI.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers.Variants
{
    public class ScreensCanvasInstaller : ScopeInstaller
    {
        [SerializeField] private ScreensController _screensController;
        
        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterComponent(_screensController).AsImplementedInterfaces().AsSelf();
        }
    }
}
