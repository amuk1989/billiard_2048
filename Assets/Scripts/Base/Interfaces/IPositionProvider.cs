using System;
using UnityEngine;

namespace Base.Interfaces
{
    public interface IPositionProvider
    {
        public IObservable<Vector3> PositionAsObservable();
        public Vector3 Position { get; }
    }
}