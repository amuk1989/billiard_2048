using System;
using UnityEngine;

namespace Input.Interface
{
    public enum TapStatus
    {
        OnRelease,
        OnDrag
    }
    
    public interface IInputService
    {
        public TapStatus TapStatus { get; }
        public IObservable<Vector2> CursorPositionAsObservable();
        public IObservable<TapStatus> TapStatusAsObservable();
        public void StartTrackInput();
        public void StopTrackInput();
    }
}