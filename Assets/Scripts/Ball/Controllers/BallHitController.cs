using Ball.Repositories;
using Zenject;

namespace Ball.Controllers
{
    public class BallHitController: IInitializable
    {
        private readonly BallModelRepository _ballModelRepository;
        
        public void Initialize()
        {
        }
    }
}