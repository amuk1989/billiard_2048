using Zenject;

namespace Rules.Bootstrap
{
    public class RulesInstaller: Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<CueRule>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<CameraRule>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<GameRule>()
                .AsSingle()
                .NonLazy();
        }
    }
}