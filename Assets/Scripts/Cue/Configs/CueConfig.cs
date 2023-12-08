using System;
using Base.Data;
using Base.Interfaces;
using Cue.Views;
using UnityEngine;

namespace Cue.Configs
{
    [CreateAssetMenu(fileName = "CueConfig", menuName = "Configs/CueConfig", order = 0)]
    public class CueConfig : BaseConfig<CueConfigData>
    {
    }

    [Serializable]
    public class CueConfigData : IConfigData
    {
        [SerializeField] private CueView _prefab;
        [SerializeField] private Vector3 _viewOffset;
        [SerializeField] private float _maxMoving;
        [SerializeField] private float _hitMoving;
        [SerializeField] private float _minEnergy;
        [SerializeField] private float _maxEnergy;
        [SerializeField] private float _cueSpeed;
        [SerializeField] private float _cueHitSpeed;

        internal CueView Prefab => _prefab;
        public Vector3 ViewOffset => _viewOffset;
        public float MaxMoving => _maxMoving;
        public float HitMoving => _hitMoving;
        public float MinEnergy => _minEnergy;
        public float CueSpeed => _cueSpeed;
        public float CueHitSpeed => _cueHitSpeed;
        public float MaxEnergy => _maxEnergy;
    }
}