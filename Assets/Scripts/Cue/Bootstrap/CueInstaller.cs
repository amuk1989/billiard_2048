using Cue.Configs;
using Cue.Controllers;
using Cue.Models;
using Cue.Services;
using Cue.Views;
using Zenject;

namespace Cue.Bootstrap
{
    public class CueInstaller: Installer
    {
        [Inject] private CueConfigData _cueConfig;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<CueService>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<CueController>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<CueModel>()
                .AsSingle()
                .NonLazy();

            Container
                .BindFactory<CueModel, CueView, CueView.Factory>()
                .FromComponentInNewPrefab(_cueConfig.Prefab);
        }
    }
}