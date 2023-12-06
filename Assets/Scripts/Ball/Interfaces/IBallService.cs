using Base.Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Ball.Interfaces
{
    public interface IBallService
    {
        public void Spawn(Vector3 position);
        public void ClearAll();
        public void SetForce(Vector3 force);
        public IPositionProvider GetMainBallPositionProvider();
    }
}