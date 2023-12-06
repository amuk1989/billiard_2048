using System;
using Ball.Data;
using Ball.Interfaces;
using Ball.Models;
using Ball.Repositories;
using Base.Interfaces;
using Camera.Interfaces;
using UnityEngine;
using Zenject;

namespace Ball.Services
{
    public class BallService: IBallService, IInitializable, IDisposable
    {
        private readonly BallModelRepository _ballModelRepository;
        private readonly ICameraService _cameraService;

        private BallModel _currentBall = null;
        private IDisposable _onHitFlow;

        private BallService(BallModelRepository ballModelRepository, ICameraService cameraService)
        {
            _ballModelRepository = ballModelRepository;
            _cameraService = cameraService;
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
            _currentBall ??= _ballModelRepository.Create(new BallData(position, _cameraService.GetPositionProvider(),
                Guid.NewGuid().ToString()));
        }

        public void ClearAll()
        {
            _currentBall = null;
            _ballModelRepository.RemoveAll();
        }

        public void SetForce(Vector3 force)
        {
            _currentBall?.SetForce(force);
            _currentBall = null;
        }

        public IPositionProvider GetMainBallPositionProvider() => _currentBall;
    }
}