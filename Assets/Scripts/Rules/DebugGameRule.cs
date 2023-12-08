using System;
using Ball.Interfaces;
using Camera.Interfaces;
using Cue.Interfaces;
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
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}