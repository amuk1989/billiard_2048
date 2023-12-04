using System;
using Base.Interfaces;
using UniRx;
using UnityEngine;

namespace Camera.Models
{
    public class CameraModel: IPositionProvider
    {
        private readonly ReactiveProperty<Vector3> _cameraPosition = new();
        
        public IObservable<Vector3> PositionAsObservable()
        {
            return _cameraPosition.AsObservable();
        }

        public Vector3 Position => _cameraPosition.Value;

        public void UpdatePosition(Vector3 position)
        {
            _cameraPosition.Value = position;
        }
    }
}