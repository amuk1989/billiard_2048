using Ball.Data;
using Ball.Models;
using Base.Repositories;
using Zenject;

namespace Ball.Repositories
{
    internal class BallModelRepository: Repository<BallData, BallModel>
    {
        internal BallModelRepository(PlaceholderFactory<BallData, BallModel> factory) : base(factory)
        {
        }

        public override BallModel Create(BallData args)
        {
            var model =  base.Create(args);
            model.Initialize();
            return model;
        }
    }
}