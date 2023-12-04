using Ball.Models;
using Ball.Views;
using Base.Interfaces;
using Base.Repositories;
using UniRx;
using Zenject;

namespace Ball.Repositories
{
    internal class BallViewRepository: Repository<BallModel, BallView>, IInitializable
    {
        private readonly IReadOnlyRepository<BallModel> _ballModelRepository;

        private readonly CompositeDisposable _compositeDisposable = new();
        
        internal BallViewRepository(PlaceholderFactory<BallModel, BallView> factory,
            IReadOnlyRepository<BallModel> ballModelRepository) : base(factory)
        {
            _ballModelRepository = ballModelRepository;
        }

        public void Initialize()
        {
            _ballModelRepository
                .OnAddedModelAsRx()
                .Subscribe(model => Create(model))
                .AddTo(_compositeDisposable);
            
            _ballModelRepository
                .OnRemoveModelAsRx()
                .Subscribe(model =>
                {
                    ModelsContainer[model.Id].Dispose();
                    Remove(model.Id);
                })
                .AddTo(_compositeDisposable);
        }
    }
}