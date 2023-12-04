using Ball.Models;
using Base.Interfaces;

namespace Ball.Data
{
    public struct BallViewData: IValueData
    {
        public string Id { get; }
        public BallModel BallModel;
        public IPositionProvider TargetPosition;

        public BallViewData(BallModel ballModel, IPositionProvider targetPosition, string id)
        {
            BallModel = ballModel;
            TargetPosition = targetPosition;
            Id = id;
        }
    }
}