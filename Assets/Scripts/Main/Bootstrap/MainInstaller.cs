using Ball.Bootstrap;
using Camera.Bootstrap;
using GameArea.Bootstrap;
using Zenject;

namespace Main.Bootstrap
{
    public class MainInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<BallsInstaller>();
            Container.Install<GameAreaInstaller>();
            Container.Install<CameraInstaller>();
        }
    }
}