using Base.Interfaces;
using UnityEngine;

namespace Ball.Data
{
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