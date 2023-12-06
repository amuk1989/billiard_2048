using Camera.Controllers;
using Camera.Models;
using Camera.Services;
using Zenject;

namespace Camera.Bootstrap
{
    public class CameraInstaller: Installer
    {
        public override void InstallBindings()
        {
            Container
                .Bind<CameraModel>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<CameraService>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<CameraController>()
                .AsSingle()
                .NonLazy();
        }
    }
}