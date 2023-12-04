using System.ComponentModel;
using Base.Interfaces;
using UnityEngine;
using Zenject;

namespace Utility
{
    public static class ZenjectExtensions
    {
        public static void InstallRegistry<TRegistry>(this DiContainer container, TRegistry registry) 
            where TRegistry: IConfigData
        {
            container
                .Bind<TRegistry>()
                .FromInstance(registry);
        }
    }
}