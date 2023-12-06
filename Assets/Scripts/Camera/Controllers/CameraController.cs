using System;
using Base.Interfaces;
using Camera.Configs;
using Camera.Interfaces;
using Camera.Models;
using Input.Interface;
using UniRx;
using UnityEngine;
using Zenject;

namespace Camera.Controllers
{
    internal class CameraController: IDisposable, IInitializable
    {
        private readonly CameraModel _cameraModel;
        private readonly CameraConfigData _cameraConfigData;
        
        private Vector3 _target;

        private IDisposable _rotateFlow;

        private CameraController(CameraModel cameraModel, CameraConfigData cameraConfigData)
        {
            _cameraModel = cameraModel;
            _cameraConfigData = cameraConfigData;
        }

        internal CameraModel Model => _cameraModel;

        public void Initialize()
        {
            _target = _cameraModel.SightDirection;
        }

        public void Dispose()
        {
            _rotateFlow?.Dispose();
        }

        public void SightTo(Vector3 target)
        {
            _target = target;
            var forward = (_target - _cameraModel.Position).normalized;
            var startForward = _cameraModel.SightDirection;
            var t = 0f;
            
            _rotateFlow?.Dispose();
            _rotateFlow = Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    if (t > 1f) _rotateFlow?.Dispose();
                    
                    var rotation = Quaternion.LookRotation(Vector3.Lerp(startForward, forward, t));
                    
                    _cameraModel.UpdateRotation(rotation);
                    
                    t += Time.deltaTime * _cameraConfigData.CameraRotationSpeed;
                });
        }

        public void RotateAroundTarget(Vector2 direction)
        {
            var radiusVector = _cameraModel.Position - _target;
            var rotate = Quaternion.Euler(0, direction.x * _cameraConfigData.CameraMovingSpeed, 0);
            
            _cameraModel.UpdatePosition(_target + rotate * radiusVector);
            
            var forward = (_target - _cameraModel.Position).normalized;
            var rotation = Quaternion.LookRotation(forward);
            
            _rotateFlow?.Dispose();
                    
            _cameraModel.UpdateRotation(rotation);
        }
    }
}