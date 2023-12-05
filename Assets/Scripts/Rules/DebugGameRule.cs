using System;
using Ball.Interfaces;
using Camera.Interfaces;
using Cysharp.Threading.Tasks;
using Input.Interface;
using UniRx;
using UnityEngine;
using Zenject;

namespace Rules
{
    public class DebugGameRule: IInitializable, IDisposable
    {
        private readonly IInputService _inputService;
        private readonly ICameraService _cameraService;
        private readonly IBallService _ballService;
        
        private readonly CompositeDisposable _compositeDisposable = new();

        public DebugGameRule(IInputService inputService, ICameraService cameraService, IBallService ballService)
        {
            _inputService = inputService;
            _cameraService = cameraService;
            _ballService = ballService;
        }

        public async void Initialize()
        {
            _ballService.Spawn(Vector3.zero);

            await UniTask.Delay(1000);
            
            var ball = _ballService.GetMainBallAsPosition();
            if (ball == null) return;
            _cameraService.LookAt(ball.Position);
            
            _inputService.StartTrackInput();
            
            _inputService
                .CursorPositionAsObservable()
                .Subscribe(value =>
                {
                    Debug.Log($"[GameRule] {value}");
                })
                .AddTo(_compositeDisposable);
            
            _inputService
                .TapStatusAsObservable()
                .Subscribe(value =>
                {
                    Debug.Log($"[GameRule] {value}");
                    
                    var ball = _ballService.GetMainBallAsPosition();
                    if (ball == null) return;
                    _cameraService.LookAt(ball.Position);
                })
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}