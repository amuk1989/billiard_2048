using System;
using Ball.Interfaces;
using Camera.Interfaces;
using Cue.Interfaces;
using Cysharp.Threading.Tasks;
using GameArea.Interfaces;
using GameStage.Interfaces;
using Input.Interface;
using UniRx;
using UnityEngine;

namespace GameStage.Stages
{
    public class GameStage: IGameStage
    {
        private readonly IInputService _inputService;
        private readonly IBallService _ballService;
        private readonly ICueService _cueService;
        private readonly ICameraService _cameraService;
        private readonly IGameArea _gameArea;

        public GameStage(IInputService inputService, IBallService ballService, ICueService cueService, 
            ICameraService cameraService, IGameArea gameArea)
        {
            _inputService = inputService;
            _ballService = ballService;
            _cueService = cueService;
            _cameraService = cameraService;
            _gameArea = gameArea;
        }

        public async void Execute()
        {
            _gameArea.Spawn();
            
            await UniTask.Yield();
            
            _ballService.Spawn(Vector3.up * 0.75f);
            _cameraService.LookAt(_ballService.GetMainBallPositionProvider().Position);
            
            _cueService.Activate();
            _cueService.SetHandler(_cameraService.GetPositionProvider());
            _cueService.SetTarget(_ballService.GetMainBallPositionProvider().Position);
            
            _inputService.StartTrackInput();
        }

        public void Complete()
        {
            _inputService.StopTrackInput();
            _gameArea.Destroy();
            _cueService.Deactivate();
        }

        public IObservable<Unit> StageCompletedAsRx()
        {
            return Observable.Never<Unit>();
        }
    }
}