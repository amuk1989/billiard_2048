using Ball.Configs;
using Base.Data;
using Base.Interfaces;
using GameArea.Data;
using UnityEngine;
using Utility;
using Zenject;

namespace Main.Bootstrap
{
    [CreateAssetMenu(fileName = "ConfigRegistry", menuName = "Registries/ConfigRegistry", order = 0)]
    public class ConfigRegistry : ScriptableObjectInstaller
    {
        [SerializeField] private BallConfig _ballConfig;
        [SerializeField] private GameAreaConfig _gameAreaConfig;
            
        public override void InstallBindings()
        {
            Container.InstallRegistry(_ballConfig.Data);
            Container.InstallRegistry(_gameAreaConfig.Data);
        }
    }
}