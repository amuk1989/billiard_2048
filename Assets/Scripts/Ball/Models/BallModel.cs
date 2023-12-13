using System;
using Ball.Configs;
using Ball.Data;
using Ball.Repositories;
using Base.Interfaces;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

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
        private int _maxIndex;

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
            var i = _configData.HitPointsData.Length/2;

            do
            {
                i = _configData.FirstBallMaxPoints < _configData.HitPointsData[i].HitPoints? i/2 : i + i/2;
            } while (_configData.FirstBallMaxPoints < _configData.HitPointsData[i].HitPoints
                     || _configData.FirstBallMaxPoints > _configData.HitPointsData[i + 1].HitPoints);
            
            _maxIndex = i;
            
            SetPoints();
        }

        private void SetPoints()
        {
            var randomIndex = Mathf.RoundToInt(Random.Range(0, _maxIndex+1));
            
            _hitPoints.Value = _configData.HitPointsData[randomIndex].HitPoints;
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