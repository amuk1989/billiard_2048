using Base.Interfaces;
using UnityEngine;

namespace Cue.Interfaces
{
    public interface ICueService
    {
        public void Activate();
        public void Deactivate();
        public void SetTarget(Vector3 target);
        public void SetHandler(IPositionProvider handler);
        public IHitting GetHitProvider();
        public void Hit();
        public void UpdateEnergy(float increment);
    }
}