using System;
using System.Collections.Generic;

namespace Base.Interfaces
{
    public interface IReadOnlyRepository<TModel> where TModel:IEntity
    {
        public IEnumerable<TModel> Models { get; }
        
        public IObservable<TModel> OnAddedModelAsRx();
        public IObservable<TModel> OnRemoveModelAsRx();
    }
}