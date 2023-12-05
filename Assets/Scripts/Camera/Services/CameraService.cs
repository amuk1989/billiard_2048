using Base.Interfaces;
using Camera.Interfaces;
using Camera.Models;
using UniRx;
using UnityEngine;

namespace Camera.Services
{
    public class CameraService: ICameraService
    {
        private readonly CameraModel _cameraModel;

        private CameraService(CameraModel cameraModel)
        {
            _cameraModel = cameraModel;
        }

        public IPositionProvider GetPositionProvider()
        {
            return _cameraModel;
        }

        public void LookAt(Vector3 target)
        {
            var forward = target - _cameraModel.Position;
            var rotation = Quaternion.LookRotation(forward);
            _cameraModel.UpdateRotation(rotation);
        }
        
        public void CreateCamera()
        {
            
        }

        public void DestroyCamera()
        {
            
        }
    }
}