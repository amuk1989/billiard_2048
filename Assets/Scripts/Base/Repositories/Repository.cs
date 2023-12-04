using System;
using System.Collections.Generic;
using Base.Interfaces;
using UniRx;
using Zenject;

namespace Base.Repositories
{
    public abstract class Repository<TArgs, TModel>: IReadOnlyRepository<TModel>
        where TModel:IEntity
        where TArgs: IValueData
    {
        private readonly PlaceholderFactory<TArgs, TModel> _factory;
        
        protected readonly ReactiveDictionary<string, TModel> ModelsContainer = new();

        protected Repository(PlaceholderFactory<TArgs, TModel> factory)
        {
            _factory = factory;
        }

        public IEnumerable<TModel> Models => ModelsContainer.Values;

        public IObservable<TModel> OnAddedModelAsRx() => ModelsContainer.ObserveAdd().Select(x => x.Value).AsObservable();
        public IObservable<TModel> OnRemoveModelAsRx() => ModelsContainer.ObserveRemove().Select(x => x.Value).AsObservable();

        public virtual TModel Create(TArgs args)
        {
            var model = _factory.Create(args);

            ModelsContainer[args.Id] = model;
            
            return model;
        }
        
        public void Remove(string id)
        {
            if (!ModelsContainer.TryGetValue(id, out _)) return;

            ModelsContainer.Remove(id);
        }

        public void RemoveAll()
        {
            ModelsContainer.Clear();
        }
    }
}