using System;
using Ball.Configs;
using Ball.Data;
using Ball.Repositories;
using Base.Interfaces;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ball.Models
{
    public class BallModel: IEntity, IInitializable, IDisposable, IPositionProvider
    {
        public string Id { get; private set; }
        public Vector3 Position => _position.Value;

        private readonly ReactiveCommand<Vector3> _force = new();
        private readonly ReactiveProperty<Vector3> _position = new();
        private readonly ReactiveProperty<uint> _hitPoints = new();

        private Vector3 _velocity;

        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly BallConfigData _configData;

        public uint HitPoints => _hitPoints.Value;
        public Vector3 Velocity => _velocity;

        public IPositionProvider TargetProvider { get; }

        private BallModel(BallData data, BallConfigData configData)
        {
            Id = data.Id;
            _position.Value = data.Position;
            TargetProvider = data.TargetPositionProvider;
            _configData = configData;
        }

        public IObservable<Vector3> ForceAsObservable() => _force.AsObservable();
        public IObservable<uint> HitPointsAsObservable() => _hitPoints.AsObservable();
        public IObservable<Vector3> PositionAsObservable() => _position.AsObservable();

        public void Initialize()
        {
            _hitPoints.Value = _configData.DefaultHitPoints;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        public void SetForce(Vector3 force)
        {
            Debug.Log(force);
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