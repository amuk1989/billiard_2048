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
            _ballService.Spawn(Vector3.up);

            _inputService.StartTrackInput();
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}