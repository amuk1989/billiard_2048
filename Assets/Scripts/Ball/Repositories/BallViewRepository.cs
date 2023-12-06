using System.Collections.Generic;
using Ball.Data;
using Ball.Models;
using Ball.Views;
using Base.Interfaces;
using Base.Repositories;
using UniRx;
using Zenject;

namespace Ball.Repositories
{
    internal class BallViewRepository: Repository<BallViewData, BallView>, IInitializable
    {
        private readonly IReadOnlyRepository<BallModel> _ballModelRepository;
        private readonly BallViewModel.Factory _ballViewModelFactory;

        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly Dictionary<string, BallViewModel> _ballViewModels = new();

        internal BallViewRepository(PlaceholderFactory<BallViewData, BallView> factory,
            IReadOnlyRepository<BallModel> ballModelRepository, BallViewModel.Factory modelFactory) : base(factory)
        {
            _ballModelRepository = ballModelRepository;
            _ballViewModelFactory = modelFactory;
        }

        public void Initialize()
        {
            _ballModelRepository
                .OnAddedModelAsRx()
                .Subscribe(model =>
                {
                    _ballViewModels[model.Id] = _ballViewModelFactory.Create(model);

                    var data = new BallViewData(_ballViewModels[model.Id], model, model.Id);
                    
                    Create(data);
                    
                    _ballViewModels[model.Id].Initialize();
                })
                .AddTo(_compositeDisposable);
            
            _ballModelRepository
                .OnRemoveModelAsRx()
                .Subscribe(model =>
                {
                    _ballViewModels.Remove(model.Id);
                    ModelsContainer[model.Id].Dispose();
                    Remove(model.Id);
                })
                .AddTo(_compositeDisposable);
        }
    }
}