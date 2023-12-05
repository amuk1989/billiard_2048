using System;
using UnityEngine;

namespace Base.Interfaces
{
    public interface IPositionProvider
    {
        public Vector3 Position { get; }
    }
}