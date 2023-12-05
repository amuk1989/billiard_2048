using System;
using Ball.Configs;
using Ball.Repositories;
using Base.Interfaces;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ball.Models
{
    public class BallModel: IEntity, IValueData, IInitializable, IDisposable
    {
        public string Id { get; private set; }
        public Vector3 Position { get; private set; }

        private readonly ReactiveCommand<Vector3> _force = new();
        private readonly ReactiveProperty<uint> _hitPoints = new();
        private readonly ReactiveProperty<Quaternion> _rotation = new();

        private Vector3 _velocity;
        private Quaternion _viewRotation;
        private bool _isRotation = false;
        
        private readonly IPositionProvider _positionProvider;
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly BallConfigData _configData;

        public uint HitPoints => _hitPoints.Value;
        public Vector3 Velocity => _velocity;

        private BallModel(BallData data, BallConfigData configData)
        {
            Id = data.Id;
            Position = data.Position;
            _positionProvider = data.TargetPositionProvider;
            _configData = configData;
        }

        public IObservable<Vector3> ForceAsObservable() => _force.AsObservable();
        public IObservable<uint> HitPointsAsObservable() => _hitPoints.AsObservable();
        public IObservable<Quaternion> RotationAsObservable() => _rotation.AsObservable();

        public void Initialize()
        {
            _hitPoints.Value = _configData.DefaultHitPoints;
            //TODO: Need to replace to some class
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    var targetRotation = Quaternion.LookRotation(_positionProvider.Position - Position);

                    _isRotation = _isRotation switch
                    {
                        //TODO: need to find some kind of comparison logic.
                        false when (targetRotation.eulerAngles - _viewRotation.eulerAngles).sqrMagnitude > 0.01 => true,
                        true when (targetRotation.eulerAngles - _viewRotation.eulerAngles).sqrMagnitude < 0.01 => false,
                        _ => _isRotation
                    };

                    if (_isRotation) _rotation.Value = Quaternion.Lerp(_viewRotation, targetRotation, Time.deltaTime * _configData.RotationSpeed);
                })
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
        
        public void UpdatePosition(Vector3 position)
        {
            Position = position;
        }
        
        public void UpdateRotation(Quaternion rotation)
        {
            _viewRotation = rotation;
        }

        public void SetForce(Vector3 force)
        {
            _force.Execute(force);
        }

        public void UpdateVelocity(Vector3 velocity)
        {
            _velocity = velocity;
        }

        public void UpgradeHitPoints(uint points)
        {
            _hitPoints.Value += points;
        }
    }
}