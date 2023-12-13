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
        [SerializeField] private uint _firstBallMaxPoints;

        [Header("View settings")] [SerializeField]
        private HitPointsPresentData[] _hitPointsData;

        public uint SleepThreshold => _sleepThreshold;
        public BallView Prefab => _prefab;
        public float RotationSpeed => _rotationSpeed;
        public uint DefaultHitPoints => _defaultHitPoints;
        public HitPointsPresentData[] HitPointsData => _hitPointsData;
        public uint FirstBallMaxPoints => _firstBallMaxPoints;
    }

    [Serializable]
    public struct HitPointsPresentData
    {
        [SerializeField] private Color _color;
        [SerializeField] private uint _hitPoints;
        
        public Color Color => _color;
        public uint HitPoints => _hitPoints;
    }
}