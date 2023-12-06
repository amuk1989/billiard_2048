using Ball.Models;
using Base.Interfaces;

namespace Ball.Data
{
    public struct BallViewData: IValueData
    {
        public string Id { get; }
        public BallModel BallModel;
        internal BallViewModel BallViewModel;

        internal BallViewData(BallViewModel ballViewModel, BallModel ballModel, string id)
        {
            BallModel = ballModel;
            Id = id;
            BallViewModel = ballViewModel;
        }
    }
}