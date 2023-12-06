using System;
using Camera.Models;
using UniRx;
using UnityEngine;
using Zenject;

namespace Camera.Views
{
    public class CameraComponent: MonoBehaviour, IDisposable
    {
        private CameraModel _cameraModel;
        
        [Inject]
        private void Construct(CameraModel cameraModel)
        {
            _cameraModel = cameraModel;
        }

        private void Awake()
        {
            _cameraModel.UpdateSightDirection(transform.forward);
            _cameraModel.UpdatePosition(transform.position);
        }

        private void Start()
        {
            _cameraModel
                .RotationAsObservable()
                .Subscribe(value =>
                {
                    transform.rotation = value;
                    _cameraModel.UpdateSightDirection(transform.forward);
                })
                .AddTo(this);

            _cameraModel
                .PositionAsObservable()
                .Subscribe(value => transform.position = value)
                .AddTo(this);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}