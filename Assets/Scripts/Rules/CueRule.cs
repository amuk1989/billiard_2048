using Base.Rules;
using Cue.Interfaces;
using Cysharp.Threading.Tasks;
using GameStage.Data;
using GameStage.Interfaces;
using Input.Interface;
using UniRx;
using UnityEngine;

namespace Rules
{
    public class CueRule: BaseGamesRule
    {
        private readonly ICueService _cueService;
        private readonly IInputService _inputService;

        private readonly CompositeDisposable _compositeDisposable = new();

        public CueRule(IGameStageService gameStageService, ICueService cueService, 
            IInputService inputService) : base(gameStageService)
        {
            _cueService = cueService;
            _inputService = inputService;
        }

        protected override void OnStageChanged(GameStageId gameStageId)
        {
            if (gameStageId == GameStageId.Game)
            {
                StartCue();
            }
            else
            {
                StopCue();
            }
        }

        private void StartCue()
        {
            _inputService
                .CursorPositionAsObservable()
                .Where(x => _inputService.TapStatus == TapStatus.OnDrag && Mathf.Abs(x.x) < 0.01f)
                .Subscribe(value => _cueService.UpdateEnergy(-value.y))
                .AddTo(_compositeDisposable);
            
            _inputService
                .TapStatusAsObservable()
                .Where(x => x == TapStatus.OnRelease)
                .Subscribe(value => _cueService.Hit())
                .AddTo(_compositeDisposable);
        }
        
        private void StopCue()
        {
            _compositeDisposable?.Dispose();
        }

        public override void Dispose()
        {
            StopCue();
            base.Dispose();
        }
    }
}