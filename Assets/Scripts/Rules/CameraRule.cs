using System;
using System.Threading.Tasks;
using Ball.Interfaces;
using Base.Rules;
using Camera.Interfaces;
using Cysharp.Threading.Tasks;
using GameStage.Data;
using GameStage.Interfaces;
using Input.Interface;
using UniRx;
using Zenject;

namespace Rules
{
    public class CameraRule: BaseGamesRule
    {
        private readonly IInputService _inputService;
        private readonly ICameraService _cameraService;
        
        private IDisposable _inputFlow;

        public CameraRule(IGameStageService gameStageService, 
            IInputService inputService, ICameraService cameraService) : base(gameStageService)
        {
            _inputService = inputService;
            _cameraService = cameraService;
        }

        protected override void OnStageChanged(GameStageId gameStageId)
        {
            if (gameStageId == GameStageId.Game)
            {
                StartCamera();
            }
            else
            {
                StopCamera();
            }
        }

        private void StartCamera()
        {
            _inputFlow = _inputService
                .CursorPositionAsObservable()
                .Where(x => _inputService.TapStatus == TapStatus.OnDrag)
                .Subscribe(value =>
                {
                    if (_inputService.TapStatus != TapStatus.OnDrag) return;

                    _cameraService.RotateAroundTarget(value);
                });
        }

        private void StopCamera()
        {
            _inputFlow?.Dispose();
        }

        public override void Dispose()
        {
            StopCamera();
            base.Dispose();
        }
    }
}