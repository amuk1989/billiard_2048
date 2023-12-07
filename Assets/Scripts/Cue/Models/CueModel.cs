using System;
using Base.Interfaces;
using UniRx;
using UnityEngine;

namespace Cue.Models
{
    internal class CueModel
    {
        private readonly ReactiveProperty<Vector3> _position = new();
        private readonly ReactiveProperty<Vector3> _target = new();

        private IDisposable _movingFlow;

        public IObservable<Vector3> PositionAsObservable() => _position.AsObservable();
        public IObservable<Vector3> TargetAsObservable() => _target.AsObservable();

        public void SetTarget(Vector3 target) => _target.Value = target;

        public void SetPositionHandler(IPositionProvider positionProvider)
        {
            _movingFlow?.Dispose();

            _movingFlow = positionProvider
                .PositionAsObservable()
                .Subscribe(value => _position.Value = value);
        }
    }
}