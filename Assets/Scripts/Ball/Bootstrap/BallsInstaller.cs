using Ball.Configs;
using Ball.Controllers;
using Ball.Models;
using Ball.Repositories;
using Ball.Services;
using Ball.Views;
using Zenject;

namespace Ball.Bootstrap
{
    public class BallsInstaller:Installer
    {
        private BallConfigData _ballConfigData;

        [Inject]
        private void Construct(BallConfigData configData)
        {
            _ballConfigData = configData;
        }
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<BallModelRepository>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<BallViewRepository>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<BallService>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<BallHitController>()
                .AsSingle()
                .NonLazy();

            Container
                .BindFactory<BallData, BallModel, PlaceholderFactory<BallData, BallModel>>();
            
            Container
                .BindFactory<BallModel, BallView, PlaceholderFactory<BallModel, BallView>>()
                .FromComponentInNewPrefab(_ballConfigData.Prefab);
        }
    }
}