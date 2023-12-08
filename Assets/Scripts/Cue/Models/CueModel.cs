using System;
using System.Threading;
using Base.Interfaces;
using Cue.Configs;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Cue.Models
{
    internal class CueModel: IDisposable, IHitting
    {
        private readonly ReactiveProperty<Vector3> _position = new();
        private readonly ReactiveProperty<Vector3> _target = new();
        private readonly ReactiveCommand<Vector3> _onHit = new();
        private readonly ReactiveProperty<float> _potentialEnergy = new();

        private readonly CueConfigData _configData;

        private IDisposable _movingFlow;
        private IPositionProvider _positionProvider;
        private CancellationTokenSource _energyUpdateToken;

        public CueModel(CueConfigData configData)
        {
            _configData = configData;
        }

        public IObservable<Vector3> PositionAsObservable() => _position.AsObservable();
        public IObservable<Vector3> TargetAsObservable() => _target.AsObservable();
        public IObservable<Vector3> OnHitAsObservable() => _onHit.AsObservable();
        public IObservable<float> EnergyAsObservable() => _potentialEnergy.AsObservable();

        public void SetTarget(Vector3 target) => _target.Value = target;

        public void SetPositionHandler(IPositionProvider positionProvider)
        {
            _movingFlow?.Dispose();
            _positionProvider = positionProvider;

            _movingFlow = _positionProvider
                .PositionAsObservable()
                .Select(x =>
                {
                    var powerDirection = (_target.Value - _position.Value).normalized * _potentialEnergy.Value*-0.01f;
                    return x + _configData.ViewOffset + powerDirection;
                })
                .Merge(this.EnergyAsObservable().Select(x =>
                {
                    var powerDirection = (_target.Value - _position.Value).normalized * x*-0.01f;
                    return _positionProvider.Position + _configData.ViewOffset + powerDirection;
                } ))
                .Subscribe(value => _position.Value = value);
        }

        public void Dispose()
        {
            _movingFlow?.Dispose();
        }

        public void UpdateEnergy(float energy)
        {
            var clampedValue = Mathf.Clamp(energy + _potentialEnergy.Value, 0, 1000);

            _energyUpdateToken = new CancellationTokenSource();
            UpdateEnergyTask(clampedValue, 10, _energyUpdateToken.Token);
        }

        public void Hit()
        {
            if (_potentialEnergy.Value <= 1) return;
            
            var direction = (_position.Value - _target.Value).normalized;
            _onHit.Execute(_potentialEnergy.Value * direction);
            _potentialEnergy.Value = 0;
            _position.Value = _positionProvider.Position + _configData.ViewOffset;
        }

        private UniTask UpdateEnergyTask(float targetValue, float speed, CancellationToken token = default)
        {
            var startValue = _potentialEnergy.Value;
            var t = 0f;
            
            return UniTask
                .WaitUntil(() =>
                    {
                        t += Time.deltaTime * speed;
                        _potentialEnergy.Value = Mathf.Lerp(startValue, targetValue, t);
                        Debug.Log($"t = {t}");
                        return t >= 1.0f;
                    }, cancellationToken: token)
                .SuppressCancellationThrow();
        }
    }
}