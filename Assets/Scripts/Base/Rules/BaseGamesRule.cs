using System;
using GameStage.Data;
using GameStage.Interfaces;
using UniRx;
using Zenject;

namespace Base.Rules
{
    public abstract class BaseGamesRule: IInitializable, IDisposable
    {
        protected readonly CompositeDisposable CompositeDisposable = new();

        private readonly IGameStageService _gameStageService;

        protected BaseGamesRule(IGameStageService gameStageService)
        {
            _gameStageService = gameStageService;
        }

        public void Initialize()
        {
            _gameStageService
                .GameStageAsObservable()
                .Subscribe(OnStageChanged)
                .AddTo(CompositeDisposable);
        }

        public virtual void Dispose()
        {
            CompositeDisposable?.Dispose();
        }

        protected abstract void OnStageChanged(GameStageId gameStageId);
    }
}