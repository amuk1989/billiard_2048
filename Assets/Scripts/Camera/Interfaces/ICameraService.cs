using Base.Interfaces;

namespace Camera.Interfaces
{
    public interface ICameraService
    {
        public IPositionProvider GetPositionProvider();
        public void CreateCamera();
        public void DestroyCamera();
    }
}