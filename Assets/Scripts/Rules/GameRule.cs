using System;
using Input.Interface;
using UniRx;
using UnityEngine;
using Zenject;

namespace Rules
{
    public class GameRule: IInitializable, IDisposable
    {
        private readonly IInputService _inputService;
        private readonly CompositeDisposable _compositeDisposable = new();

        public GameRule(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Initialize()
        {
            _inputService.StartTrackInput();
            _inputService
                .CursorPositionAsObservable()
                .Subscribe(value =>
                {
                    Debug.Log($"[GameRule] {value}");
                })
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}