using System;
using UnityEngine;

namespace Input.Interface
{
    public interface IInputService
    {
        public IObservable<Vector2> CursorPositionAsObservable();
        public void StartTrackInput();
        public void StopTrackInput();
    }
}