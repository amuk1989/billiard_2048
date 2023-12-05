using System;
using Ball.Views;
using Base.Data;
using Base.Interfaces;
using UnityEngine;

namespace Ball.Configs
{
    [CreateAssetMenu(fileName = "BallConfig", menuName = "Configs/BallConfig", order = 0)]
    public class BallConfig : BaseConfig<BallConfigData>
    {
    }

    [Serializable]
    public class BallConfigData: IConfigData
    {
        [SerializeField] private BallView _prefab;
        [Range(0, 100)] [SerializeField] private uint _sleepThreshold = 25;
        [Range(0, 3)] [SerializeField] private float _rotationSpeed = 1f;
        [Range(0, 1024)] [SerializeField] private uint _defaultHitPoints = 128;

        public uint SleepThreshold => _sleepThreshold;
        public BallView Prefab => _prefab;
        public float RotationSpeed => _rotationSpeed;
        public uint DefaultHitPoints => _defaultHitPoints;
    } 
}