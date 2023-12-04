using System;
using Camera.Models;
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

        private void Update()
        {
            _cameraModel.UpdatePosition(transform.position);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}