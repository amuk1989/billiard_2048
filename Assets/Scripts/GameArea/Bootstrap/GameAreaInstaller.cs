using GameArea.Data;
using GameArea.Services;
using GameArea.Views;
using Zenject;

namespace GameArea.Bootstrap
{
    public class GameAreaInstaller: Installer
    {
        private GameAreaConfigData _gameAreaConfigData;
        
        [Inject]
        private void Construct(GameAreaConfigData gameAreaConfigData)
        {
            _gameAreaConfigData = gameAreaConfigData;
        }
        
        public override void InstallBindings()
        {
            Container
                .BindFactory<GameAreaView, GameAreaView.Factory>()
                .FromComponentInNewPrefab(_gameAreaConfigData.GameAreaView);

            Container
                .BindInterfacesTo<GameAreaService>()
                .AsSingle()
                .NonLazy();
        }
    }
}