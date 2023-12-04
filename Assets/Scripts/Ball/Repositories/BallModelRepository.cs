using Ball.Models;
using Base.Interfaces;
using Base.Repositories;
using UnityEngine;
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

    public struct BallData : IValueData
    {
        public string Id { get; }
        public readonly Vector3 Position;
        public readonly IPositionProvider TargetPositionProvider;

        public BallData(Vector3 position, IPositionProvider targetPositionProvider, string id = "")
        {
            Id = id;
            Position = position;
            TargetPositionProvider = targetPositionProvider;
        }
    }
}