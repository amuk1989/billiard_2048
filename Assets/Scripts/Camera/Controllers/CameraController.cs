using Base.Interfaces;
using Camera.Interfaces;
using Camera.Models;
using Input.Interface;
using UnityEngine;

namespace Camera.Controllers
{
    public class CameraController
    {
        private readonly ICameraService _cameraService;

        private readonly Vector3 _target;

        public CameraController(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public void SightTo(Vector3 target)
        {
        }
    }
}