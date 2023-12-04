using System;
using Ball.Repositories;
using Base.Interfaces;
using HitMechanic.Interfaces;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ball.Models
{
    public class BallModel: IEntity, IValueData, IHittable
    {
        public string Id { get; private set; }
        public Vector3 Position { get; private set; }

        private readonly ReactiveCommand<Vector3> _force = new();
        private readonly ReactiveProperty<uint> _hitPoints = new();

        private Vector3 _velocity;

        public uint HitPoints => _hitPoints.Value;
        public Vector3 Velocity => _velocity;

        private BallModel(BallData data)
        {
            Id = data.Id;
            Position = data.Position;
        }

        public IObservable<Vector3> ForceAsObservable() => _force.AsObservable();
        public IObservable<uint> HitPointsAsObservable() => _hitPoints.AsObservable();

        public void UpdatePosition(Vector3 position)
        {
            Position = position;
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