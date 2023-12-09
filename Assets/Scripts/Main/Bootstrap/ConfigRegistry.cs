using Ball.Configs;
using Camera.Configs;
using Cue.Configs;
using GameArea.Data;
using Input.Configs;
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
        [SerializeField] private CameraConfig _cameraConfigData;
        [SerializeField] private CueConfig _cueConfig;
        [SerializeField] private InputConfig _inputConfig;
            
        public override void InstallBindings()
        {
            Container.InstallRegistry(_ballConfig.Data);
            Container.InstallRegistry(_gameAreaConfig.Data);
            Container.InstallRegistry(_cameraConfigData.Data);
            Container.InstallRegistry(_cueConfig.Data);
            Container.InstallRegistry(_inputConfig.Data);
        }
    }
}