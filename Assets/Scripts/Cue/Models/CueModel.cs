using System;
using Base.Interfaces;
using Cue.Configs;
using UniRx;
using UnityEngine;

namespace Cue.Models
{
    internal class CueModel: IDisposable
    {
        private readonly ReactiveProperty<Vector3> _position = new();
        private readonly ReactiveProperty<Vector3> _target = new();

        private readonly CueConfigData _configData;

        private IDisposable _movingFlow;

        public CueModel(CueConfigData configData)
        {
            _configData = configData;
        }

        public IObservable<Vector3> PositionAsObservable() => _position.AsObservable();
        public IObservable<Vector3> TargetAsObservable() => _target.AsObservable();

        public void SetTarget(Vector3 target) => _target.Value = target;

        public void SetPositionHandler(IPositionProvider positionProvider)
        {
            _movingFlow?.Dispose();

            _movingFlow = positionProvider
                .PositionAsObservable()
                .Subscribe(value => _position.Value = value + _configData.ViewOffset);
        }

        public void Dispose()
        {
            _movingFlow?.Dispose();
        }

        public void SetForce()
        {
            
        }

        public void Hit()
        {
            
        }
    }
}