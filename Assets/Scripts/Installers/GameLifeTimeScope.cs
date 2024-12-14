using System.Collections.Generic;
using Installers.Variants;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private List<ScopeInstaller> _scopes;

        protected override void Configure(IContainerBuilder builder)
        {
            _scopes.ForEach(x=>x.Install(builder));
        }
    }
}
