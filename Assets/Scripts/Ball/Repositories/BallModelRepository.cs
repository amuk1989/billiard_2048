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
    }

    public struct BallData : IValueData
    {
        public string Id { get; }
        public readonly Vector3 Position;

        public BallData(Vector3 position, string id = "")
        {
            Id = id;
            Position = position;
        }
    }
}