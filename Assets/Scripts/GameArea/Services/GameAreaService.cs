using GameArea.Interfaces;

namespace GameArea.Services
{
    public class GameAreaService: IGameArea
    {
        private readonly GameAreaView.Factory _factory;

        private GameAreaView _gameAreaView;

        public GameAreaService(GameAreaView.Factory factory)
        {
            _factory = factory;
        }

        public void Spawn()
        {
            _gameAreaView = _factory.Create();
        }

        public void Destroy()
        {
            if (_gameAreaView != null) _gameAreaView.Dispose();
        }
    }
}