using Base.Interfaces;
using UnityEngine;
using Utility;
using Zenject;

namespace Base.Data
{
    public abstract class BaseConfig<TData> : ScriptableObject where TData: class, IConfigData
    {
        [SerializeField] private TData _data;

        public TData Data => _data;
    }
}