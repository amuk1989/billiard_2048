using System;
using Base.Interfaces;
using UniRx;
using UnityEngine;

namespace Camera.Models
{
    internal class CameraModel: IPositionProvider
    {
        private readonly ReactiveProperty<Vector3> _cameraPosition = new();
        private readonly ReactiveProperty<Quaternion> _cameraRotation = new();
        
        public IObservable<Vector3> PositionAsObservable()
        {
            return _cameraPosition.AsObservable();
        }
        
        public IObservable<Quaternion> RotationAsObservable()
        {
            return _cameraRotation.AsObservable();
        }

        public Vector3 Position => _cameraPosition.Value;

        public void UpdatePosition(Vector3 position)
        {
            _cameraPosition.Value = position;
        }
        
        public void UpdateRotation(Quaternion rotation)
        {
            _cameraRotation.Value = rotation;
        }
    }
}