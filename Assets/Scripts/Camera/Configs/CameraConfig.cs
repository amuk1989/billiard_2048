using System;
using Base.Data;
using Base.Interfaces;
using UnityEngine;

namespace Camera.Configs
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "Configs/CameraConfig", order = 0)]
    public class CameraConfig : BaseConfig<CameraConfigData>
    {
        
    }

    [Serializable]
    public class CameraConfigData : IConfigData
    {
        [Range(0,20)][SerializeField] private float _cameraMovingSpeed;
        [Range(0,2)][SerializeField] private float _cameraRotationSpeed;
        [Range(0, 1)] [SerializeField] private float _cameraInputSensitivity;

        public float CameraMovingSpeed => _cameraMovingSpeed;
        public float CameraRotationSpeed => _cameraRotationSpeed;
        public float CameraInputSensitivity => _cameraInputSensitivity;
    }
}