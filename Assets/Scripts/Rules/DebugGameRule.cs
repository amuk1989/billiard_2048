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
        private readonly IBallService _ballService;
        
        private readonly CompositeDisposable _compositeDisposable = new();

        public DebugGameRule(IInputService inputService, IBallService ballService)
        {
            _inputService = inputService;
            _ballService = ballService;
        }

        public void Initialize()
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