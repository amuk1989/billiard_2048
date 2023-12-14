using System;
using Cysharp.Threading.Tasks;
using GameStage.Interfaces;
using UniRx;

namespace GameStage.Stages
{
    public class MainMenuStage: IGameStage
    {
        private readonly ReactiveCommand _onComplete = new();
        public async void Execute()
        {
            await UniTask.Delay(500);
            _onComplete.Execute();
        }

        public void Complete()
        {
            
        }

        public IObservable<Unit> StageCompletedAsRx() => _onComplete.AsObservable();
    }
}