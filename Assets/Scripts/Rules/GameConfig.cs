using System;
using Base.Data;
using Base.Interfaces;
using UnityEngine;

namespace Rules
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 0)]
    public class GameConfig : BaseConfig<GameConfigData>
    {
        
    }

    [Serializable]
    public class GameConfigData : IConfigData
    {
        [SerializeField] private uint _maxBallCount;
    }
}