using System;
using Input.Interface;
using UniRx;
using UnityEngine;
using Utility;

namespace Input.Services
{
    public class InputService: IInputService, IDisposable
    {
        private readonly ReactiveProperty<Vector2> _cursorPosition = new();
        private readonly ReactiveProperty<TapStatus> _tapStatus = new(TapStatus.OnRelease);
        
        private IDisposable _inputFlow;
        private Vector2 _lastPosition;

        public IObservable<Vector2> CursorPositionAsObservable() => _cursorPosition.AsObservable();
        public IObservable<TapStatus> TapStatusAsObservable() => _tapStatus.AsObservable();

        public void StartTrackInput()
        {
            _lastPosition = UnityEngine.Input.mousePosition;
            _inputFlow = Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    _cursorPosition.Value = (Vector2)UnityEngine.Input.mousePosition - _lastPosition;
                    _lastPosition = UnityEngine.Input.mousePosition;

                    if (UnityEngine.Input.GetMouseButtonDown(0)) _tapStatus.Value = TapStatus.OnDrag;
                    if (UnityEngine.Input.GetMouseButtonUp(0)) _tapStatus.Value = TapStatus.OnRelease;
                });
        }

        public void StopTrackInput()
        {
            _inputFlow?.Dispose();
        }

        public void Dispose()
        {
            StopTrackInput();
        }
    }
}