using System;
using Base.Data;
using Base.Interfaces;
using UnityEngine;

namespace Input.Configs
{
    [CreateAssetMenu(fileName = "InputConfig", menuName = "Configs/InputConfig", order = 0)]
    public class InputConfig : BaseConfig<InputConfigData>
    {
        
    }

    [Serializable]
    public class InputConfigData : IConfigData
    {
        [Range(0, 1)] [SerializeField] private float _inputSensitivity;

        public float InputSensitivity => _inputSensitivity;
    }
}