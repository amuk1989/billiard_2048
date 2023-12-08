using System;
using Ball.Interfaces;
using Camera.Interfaces;
using Cue.Interfaces;
using Cysharp.Threading.Tasks;
using Input.Interface;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Rules
{
    public class DebugGameRule: IInitializable, IDisposable
    {
        private readonly IInputService _inputService;
        private readonly IBallService _ballService;
        private readonly ICueService _cueService;
        private readonly ICameraService _cameraService;
        
        private readonly CompositeDisposable _compositeDisposable = new();

        public DebugGameRule(IInputService inputService, IBallService ballService, ICueService cueService,
            ICameraService cameraService)
        {
            _inputService = inputService;
            _ballService = ballService;
            _cueService = cueService;
            _cameraService = cameraService;
        }

        public async void Initialize()
        {
            _ballService.Spawn(Vector3.up);
            
            await UniTask.WaitUntil(() => _ballService.GetMainBallPositionProvider() != null);

            _inputService.StartTrackInput();
            
            _cueService.Activate();
            _cueService.SetHandler(_cameraService.GetPositionProvider());
            _cueService.SetTarget(_ballService.GetMainBallPositionProvider().Position);

            var hitProvider = _cueService.GetHitProvider();

            _inputService
                .CursorPositionAsObservable()
                .Where(x => _inputService.TapStatus == TapStatus.OnDrag && Mathf.Abs(x.y) > 0.01f)
                .Subscribe(value => hitProvider.UpdateEnergy(-value.y))
                .AddTo(_compositeDisposable);
            
            _inputService
                .TapStatusAsObservable()
                .Where(x => x == TapStatus.OnRelease)
                .Subscribe(value => _cueService.Hit())
                .AddTo(_compositeDisposable);
            
            hitProvider
                .OnHitAsObservable()
                .Subscribe(value => _ballService.SetForce(-value))
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}