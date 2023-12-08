using System;
using Base.Interfaces;
using Cue.Configs;
using UniRx;
using UnityEngine;

namespace Cue.Models
{
    internal class CueModel: IDisposable, IHitProvider
    {
        private readonly ReactiveProperty<Vector3> _position = new();
        private readonly ReactiveProperty<Vector3> _target = new();
        private readonly ReactiveCommand<Vector3> _onHit = new();

        private readonly CueConfigData _configData;

        private IDisposable _movingFlow;
        private IPositionProvider _positionProvider;
        private volatile float _potentialEnergy;

        public CueModel(CueConfigData configData)
        {
            _configData = configData;
        }

        public IObservable<Vector3> PositionAsObservable() => _position.AsObservable();
        public IObservable<Vector3> TargetAsObservable() => _target.AsObservable();
        public IObservable<Vector3> OnHitAsObservable() => _onHit.AsObservable();

        public void SetTarget(Vector3 target) => _target.Value = target;

        public void SetPositionHandler(IPositionProvider positionProvider)
        {
            _movingFlow?.Dispose();
            _positionProvider = positionProvider;

            _movingFlow = _positionProvider
                .PositionAsObservable()
                .Subscribe(value =>
                {
                    var powerDirection = (_target.Value - _position.Value).normalized * _potentialEnergy*-0.01f;
                    _position.Value = value + _configData.ViewOffset + powerDirection;
                });
        }

        public void Dispose()
        {
            _movingFlow?.Dispose();
        }

        public void UpdateEnergy(float energy)
        {
            _potentialEnergy = Mathf.Clamp(energy + _potentialEnergy, 0, 1000);
        }

        public void Hit()
        {
            if (_potentialEnergy <= 1) return;
            
            var direction = (_position.Value - _target.Value).normalized;
            _onHit.Execute(_potentialEnergy * direction);
            _potentialEnergy = 0;
            _position.Value = _positionProvider.Position + _configData.ViewOffset;
        }
    }
}