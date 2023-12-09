using Base.Interfaces;
using Camera.Controllers;
using Camera.Interfaces;
using UnityEngine;

namespace Camera.Services
{
    public class CameraService: ICameraService
    {
        private readonly CameraController _cameraController;

        private CameraService(CameraController cameraController)
        {
            _cameraController = cameraController;
        }

        public IPositionProvider GetPositionProvider()
        {
            return _cameraController.Model;
        }

        public void LookAt(Vector3 target)
        {
            _cameraController.SightTo(target);
        }

        public void RotateAroundTarget(Vector2 direction)
        {
            _cameraController.RotateAroundTarget(direction);
        }
        
        public void CreateCamera()
        {
            
        }

        public void DestroyCamera()
        {
            
        }
    }
}