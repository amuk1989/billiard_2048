using System;
using System.Threading.Tasks;
using Ball.Interfaces;
using Ball.Models;
using Ball.Repositories;
using UnityEngine;
using UniRx;
using Zenject;

namespace Ball.Services
{
    public class BallService: IBallService, IInitializable, IDisposable
    {
        private readonly BallModelRepository _ballModelRepository;

        private BallModel _currentBall = null;
        private IDisposable _onHitFlow;

        private BallService(BallModelRepository ballModelRepository)
        {
            _ballModelRepository = ballModelRepository;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
            _onHitFlow?.Dispose();
        }

        public void Spawn(Vector3 position)
        {
            _currentBall = _ballModelRepository.Create(new BallData(position, Guid.NewGuid().ToString()));
        }

        public void ClearAll()
        {
            _currentBall = null;
            _ballModelRepository.RemoveAll();
        }

        public void SetForce(Vector3 force)
        {
            _currentBall?.SetForce(force);
        }
    }
}