using GameStage.Controllers;
using GameStage.Data;
using GameStage.Factories;
using GameStage.Interfaces;
using Zenject;

namespace GameStage.Bootstrap
{
    public class GameStageInstaller: Installer
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IGameStage>()
                .WithId(GameStageId.Game)
                .To<Stages.GameStage>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<GameStageController>()
                .AsSingle()
                .NonLazy();

            Container
                .BindFactory<GameStageId, IGameStage, GameStageFactory>()
                .FromFactory<GameStageInstanceFactory>();
        }
    }
}