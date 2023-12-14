using Ball.Bootstrap;
using Camera.Bootstrap;
using Cue.Bootstrap;
using GameArea.Bootstrap;
using GameStage.Bootstrap;
using Input.Bootstrap;
using Rules.Bootstrap;
using Zenject;

namespace Main.Bootstrap
{
    public class MainInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<GameStageInstaller>();
            Container.Install<BallsInstaller>();
            Container.Install<GameAreaInstaller>();
            Container.Install<CameraInstaller>();
            Container.Install<CueInstaller>();
            Container.Install<RulesInstaller>();
            Container.Install<InputInstaller>();
        }
    }
}