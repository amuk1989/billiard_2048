using System;
using Ball.Interfaces;
using GameArea.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ball.Tests.Manual
{
    public class TestUI : MonoBehaviour
    {
        [SerializeField] private Button _spawnButton;
        [SerializeField] private Button _forceButton;

        private IBallService _ballService;
        private IGameArea _gameArea;

        [Inject]
        private void Construct(IBallService ballService, IGameArea gameArea)
        {
            _ballService = ballService;
            _gameArea = gameArea;
        }
        
        private void Start()
        {
            _spawnButton.onClick.AddListener(OnSpawnClick); 
            _forceButton.onClick.AddListener(OnForceClick);
            
            _gameArea.Spawn();
        }

        private void OnSpawnClick()
        {
            _ballService.Spawn(Vector3.up);
        }

        private void OnForceClick()
        {
            _ballService.SetForce(Vector3.forward * 1000);
        }

        private void OnDestroy()
        {
            _spawnButton.onClick.RemoveAllListeners(); 
            _gameArea.Destroy();
        }
    }
}