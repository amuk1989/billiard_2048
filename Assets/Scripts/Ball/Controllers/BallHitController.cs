using System;
using System.Collections.Generic;
using Ball.Models;
using Ball.Repositories;
using UniRx;
using Zenject;

namespace Ball.Controllers
{
    internal class BallHitController
    {
        private readonly BallModelRepository _ballModelRepository;

        public BallHitController(BallModelRepository ballModelRepository)
        {
            _ballModelRepository = ballModelRepository;
        }

        public void OnHit(BallModel model, BallModel hitModel)
        {
            if (model.HitPoints != hitModel.HitPoints) return;
            
            BallModel destroyBallModel;
            BallModel upgradeBallModel;
            
            if (model.Velocity.sqrMagnitude > hitModel.Velocity.sqrMagnitude)
            {
                destroyBallModel = hitModel;
                upgradeBallModel = model;
            }
            else
            {
                destroyBallModel = model;
                upgradeBallModel = hitModel;
            }
            
            upgradeBallModel.UpgradeHitPoints(destroyBallModel.HitPoints);
            
            _ballModelRepository.Remove(destroyBallModel.Id);
            
        }
    }
}