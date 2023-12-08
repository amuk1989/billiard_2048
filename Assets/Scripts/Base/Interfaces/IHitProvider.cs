using System;
using UnityEngine;

namespace Base.Interfaces
{
    public interface IHitProvider
    {
        public IObservable<Vector3> OnHitAsObservable();
        public void UpdateEnergy(float energy);
    }
}