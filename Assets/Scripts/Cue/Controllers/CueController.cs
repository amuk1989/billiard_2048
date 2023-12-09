using System;
using System.Threading;
using Base.Interfaces;
using Cue.Configs;
using Cue.Models;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Cue.Controllers
{
    internal class CueController: IDisposable
    {
        private readonly CueModel _model;
        private readonly CueConfigData _configData;

        private IDisposable _movingFlow;
        private IPositionProvider _positionProvider;
        private CancellationTokenSource _energyUpdateToken;

        private readonly float _movingEnergyRatio;

        private CueController(CueModel model, CueConfigData configData)
        {
            _model = model;
            _configData = configData;

            _movingEnergyRatio = _configData.MaxMoving / _configData.MaxEnergy;
        }

        public void SetTargetHandler(IPositionProvider positionProvider)
        {
            _movingFlow?.Dispose();
            _positionProvider = positionProvider;

            _movingFlow = _positionProvider
                .PositionAsObservable()
                .Select(x =>
                {
                    var powerDirection = (_model.Position - _model.Target).normalized * _model.Energy * _movingEnergyRatio;
                    return x + _configData.ViewOffset + powerDirection;
                })
                .Merge(_model.EnergyAsObservable().Select(x =>
                {
                    var powerDirection = (_model.Position - _model.Target).normalized * x * _movingEnergyRatio;
                    return _positionProvider.Position + _configData.ViewOffset + powerDirection;
                } ))
                .Subscribe(_model.SetPosition);
        }
        
        public void UpdateEnergy(float increment)
        {
            var clampedValue = Mathf.Clamp(increment + _model.Energy, 0, _configData.MaxEnergy);

            StartEnergyUpdateTask(clampedValue, _configData.CueSpeed);
        }

        public void Hit()
        {
            _model.Hit();
            
            StartEnergyUpdateTask(0, _configData.CueHitSpeed);
        }
        
        private void StartEnergyUpdateTask(float targetValue, float speed)
        {
            _energyUpdateToken?.Cancel();
            _energyUpdateToken?.Dispose();
            _energyUpdateToken = new CancellationTokenSource();

            UpdateEnergyTask(targetValue, speed, _energyUpdateToken.Token).SuppressCancellationThrow();
        }
        
        private UniTask UpdateEnergyTask(float targetValue, float speed, CancellationToken token = default)
        {
            var startValue = _model.Energy;
            var t = Mathf.PI * 0.5f;
            
            return UniTask
                .WaitUntil(() =>
                {
                    t -= Time.deltaTime * speed;
                    _model.SetEnergy(Mathf.Lerp(startValue, targetValue, Mathf.Cos(t)));
                    
                    return t < 0f;
                }, cancellationToken: token);
        }
        
        public void Dispose()
        {
            _movingFlow?.Dispose();
        }
    }
}