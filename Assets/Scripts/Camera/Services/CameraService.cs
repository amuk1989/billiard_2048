using Base.Interfaces;
using Camera.Interfaces;
using Camera.Models;
using UniRx;

namespace Camera.Services
{
    public class CameraService: ICameraService
    {
        private readonly CameraModel _cameraModel;

        public CameraService(CameraModel cameraModel)
        {
            _cameraModel = cameraModel;
        }

        public IPositionProvider GetPositionProvider()
        {
            return _cameraModel;
        }

        public void CreateCamera()
        {
            
        }

        public void DestroyCamera()
        {
            
        }
    }
}