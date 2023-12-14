using System.Threading;
using Ball.Interfaces;
using Base.Rules;
using Cue.Interfaces;
using Cysharp.Threading.Tasks;
using GameStage.Data;
using GameStage.Interfaces;
using UniRx;
using UnityEngine;

namespace Rules
{
    public class GameRule: BaseGamesRule
    {
        private readonly IBallService _ballService;
        private readonly ICueService _cueService;
        
        private readonly CompositeDisposable _compositeDisposable = new();
        private CancellationTokenSource _tokenSource;

        public GameRule(IGameStageService gameStageService, IBallService ballService, 
            ICueService cueService) : base(gameStageService)
        {
            _ballService = ballService;
            _cueService = cueService;
        }

        protected override void OnStageChanged(GameStageId gameStageId)
        {
            if (gameStageId == GameStageId.Game)
            {
                _tokenSource = new CancellationTokenSource();
                
                _cueService.GetHitProvider()
                    .OnHitAsObservable()
                    .Subscribe(OnHit)
                    .AddTo(_compositeDisposable);
            }
            else
            {
                StopGame();
            }
        }

        private void StopGame()
        {
            _compositeDisposable?.Dispose();
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
            _tokenSource = null;
        }

        private async void OnHit(Vector3 value)
        {
            _ballService.SetForce(-value * 100);
            await UniTask
                .Delay(3000, cancellationToken: _tokenSource.Token)
                .SuppressCancellationThrow();
            _ballService.Spawn(Vector3.up*0.75f);
        }

        public override void Dispose()
        {
            StopGame();
            base.Dispose();
        }
    }
}