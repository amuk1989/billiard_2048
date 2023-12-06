using Cue.Services;
using Zenject;

namespace Cue.Bootstrap
{
    public class CueInstaller: Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<CueService>()
                .AsSingle()
                .NonLazy();
        }
    }
}