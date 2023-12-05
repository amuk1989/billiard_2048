using Input.Interface;
using Input.Services;
using Zenject;

namespace Input.Bootstrap
{
    public class InputInstaller:Installer
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IInputService>()
                .To<InputService>()
                .AsSingle()
                .NonLazy();
        }
    }
}