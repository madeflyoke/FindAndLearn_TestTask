using UnityEngine;
using VContainer;

namespace Installers.Variants
{
    public abstract class ScopeInstaller : MonoBehaviour
    {
        public abstract void Install(IContainerBuilder builder);
    }
}
