using System;
using GameStage.Data;
using GameStage.Factories;
using GameStage.Interfaces;
using UniRx;
using Zenject;

namespace GameStage.Controllers
{
    internal class GameStageController: IGameStageService, IInitializable
    {
        private IGameStage _currentStage;
        private int _currentStageIndex = -1;

        private readonly GameStageConfigData _gameStageConfig;
        private readonly GameStageFactory _gameStageFactory;

        private readonly ReactiveProperty<GameStageId> _currentStageId = new ();

        private IDisposable _stageCompleteFlow;

        public GameStageController(GameStageConfigData gameStageConfig, GameStageFactory gameStageFactory)
        {
            _gameStageConfig = gameStageConfig;
            _gameStageFactory = gameStageFactory;
        }

        public IObservable<GameStageId> GameStageAsObservable() => _currentStageId.AsObservable();

        public void Initialize()
        {
            NextStage();
        }

        public void NextStage()
        {
            _stageCompleteFlow?.Dispose();
            _currentStage?.Complete();

            if (_gameStageConfig.GameStageIds.Length <= ++_currentStageIndex) _currentStageIndex = 0;

            var stageId = _gameStageConfig.GameStageIds[_currentStageIndex];
            
            _currentStage = _gameStageFactory.Create(stageId);
            _currentStage.Execute();
            _currentStageId.Value = stageId;

            _stageCompleteFlow = _currentStage
                .StageCompletedAsRx()
                .Subscribe(_ => NextStage());
        }
    }
}