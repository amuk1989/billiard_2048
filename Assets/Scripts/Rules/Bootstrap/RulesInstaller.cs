using Zenject;

namespace Rules.Bootstrap
{
    public class RulesInstaller: Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<DebugGameRule>()
                .AsSingle()
                .NonLazy();
        }
    }
}