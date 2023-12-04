using System;
using Base.Data;
using Base.Interfaces;
using UnityEngine;

namespace GameArea.Data
{
    [CreateAssetMenu(fileName = "GameAreaConfig", menuName = "Configs/GameAreaConfig", order = 0)]
    public class GameAreaConfig: BaseConfig<GameAreaConfigData>
    {
        
    }

    [Serializable]
    public class GameAreaConfigData : IConfigData
    {
        [SerializeField] private GameAreaView _gameAreaView;

        public GameAreaView GameAreaView => _gameAreaView;
    }
}