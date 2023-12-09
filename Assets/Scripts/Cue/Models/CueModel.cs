using System;
using System.Threading;
using Base.Interfaces;
using Cue.Configs;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Cue.Models
{
    internal class CueModel: IHitting
    {
        private readonly ReactiveProperty<Vector3> _position = new();
        private readonly ReactiveProperty<Vector3> _target = new();
        private readonly ReactiveCommand<Vector3> _onHit = new();
        private readonly ReactiveProperty<float> _potentialEnergy = new();

        private readonly CueConfigData _configData;
        
        private IPositionProvider _positionProvider;
        private CancellationTokenSource _energyUpdateToken;

        public CueModel(CueConfigData configData)
        {
            _configData = configData;
        }

        public Vector3 Target => _target.Value;
        public Vector3 Position => _position.Value;
        public float Energy => _potentialEnergy.Value;

        public IObservable<Vector3> PositionAsObservable() => _position.AsObservable();
        public IObservable<Vector3> TargetAsObservable() => _target.AsObservable();
        public IObservable<Vector3> OnHitAsObservable() => _onHit.AsObservable();
        public IObservable<float> EnergyAsObservable() => _potentialEnergy.AsObservable();

        public void SetTarget(Vector3 target) => _target.Value = target;
        public void SetEnergy(float energy) => _potentialEnergy.Value = energy;
        public void SetPosition(Vector3 position) => _position.Value = position;

        public void Hit()
        {
            if (_potentialEnergy.Value <= _configData.MinEnergy) return;
            
            var direction = (_position.Value - _target.Value).normalized;
            _onHit.Execute(_potentialEnergy.Value * direction);
        }
    }
}