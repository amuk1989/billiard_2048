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

        public uint SleepThreshold => _sleepThreshold;
        public BallView Prefab => _prefab;
    } 
}