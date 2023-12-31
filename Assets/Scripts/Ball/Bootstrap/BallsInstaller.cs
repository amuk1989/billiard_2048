﻿using Ball.Configs;
using Ball.Data;
using Ball.Models;
using Ball.Repositories;
using Ball.Services;
using Ball.Utilities;
using Ball.Views;
using Base.Interfaces;
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
                .BindInterfacesAndSelfTo<BallHitUtility>()
                .AsSingle()
                .NonLazy();

            Container
                .BindFactory<BallData, BallModel, PlaceholderFactory<BallData, BallModel>>();
            
            Container
                .BindFactory<BallModel, BallViewModel, BallViewModel.Factory>();
            
            Container
                .BindFactory<BallViewData, BallView, PlaceholderFactory<BallViewData, BallView>>()
                .FromComponentInNewPrefab(_ballConfigData.Prefab);
        }
    }
}