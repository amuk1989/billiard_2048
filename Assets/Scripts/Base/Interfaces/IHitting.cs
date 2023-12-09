using System;
using UnityEngine;

namespace Base.Interfaces
{
    public interface IHitting
    {
        public IObservable<Vector3> OnHitAsObservable();
        public IObservable<float> EnergyAsObservable();
    }
}