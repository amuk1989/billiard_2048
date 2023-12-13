using System;
using Ball.Interfaces;
using Base.Rules;
using Camera.Interfaces;
using Cue.Interfaces;
using Cysharp.Threading.Tasks;
using GameStage.Data;
using GameStage.Interfaces;
using Input.Interface;
using UniRx;
using UnityEngine;

namespace Rules
{
    public class GameRule: BaseGamesRule
    {
        private readonly IBallService _ballService;
        private readonly ICueService _cueService;
        
        private readonly CompositeDisposable _compositeDisposable = new();


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
                _cueService.GetHitProvider()
                    .OnHitAsObservable()
                    .Subscribe(async value =>
                    {
                        _ballService.SetForce(-value * 100);
                        await UniTask.Delay(3000);
                        _ballService.Spawn(Vector3.up*0.75f);
                    })
                    .AddTo(_compositeDisposable);
            }
            else
            {
                _compositeDisposable?.Dispose();
            }
        }


        public override void Dispose()
        {
            _compositeDisposable?.Dispose();
            base.Dispose();
        }
    }
}