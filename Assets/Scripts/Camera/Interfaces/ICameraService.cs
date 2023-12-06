using Base.Interfaces;
using UnityEngine;

namespace Camera.Interfaces
{
    public interface ICameraService
    {
        public IPositionProvider GetPositionProvider();
        public void CreateCamera();
        public void DestroyCamera();
        public void LookAt(Vector3 target);
        public void RotateAroundTarget(Vector2 direction);
    }
}