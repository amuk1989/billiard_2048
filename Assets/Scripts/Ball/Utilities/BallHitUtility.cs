using Ball.Models;
using Ball.Repositories;

namespace Ball.Utilities
{
    internal class BallHitUtility
    {
        private readonly BallModelRepository _ballModelRepository;

        public BallHitUtility(BallModelRepository ballModelRepository)
        {
            _ballModelRepository = ballModelRepository;
        }

        public void OnHit(BallModel model, BallModel hitModel)
        {
            if (model.HitPoints != hitModel.HitPoints) return;

            if (model.Velocity.sqrMagnitude >= hitModel.Velocity.sqrMagnitude)
            {
                UpdateBalls(hitModel, model);
            }
            else
            {
                UpdateBalls(model, hitModel);
            }
        }

        private void UpdateBalls(BallModel destroyBallModel, BallModel upgradeBallModel)
        {
            upgradeBallModel.UpgradeHitPoints(destroyBallModel.HitPoints);
            
            _ballModelRepository.Remove(destroyBallModel.Id);
        }
    }
}