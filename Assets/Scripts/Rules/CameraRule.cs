using Ball.Interfaces;
using Camera.Interfaces;
using Cysharp.Threading.Tasks;
using Input.Interface;
using UniRx;
using Zenject;

namespace Rules
{
    public class CameraRule: IInitializable
    {
        private readonly IInputService _inputService;
        private readonly ICameraService _cameraService;
        private readonly IBallService _ballService;

        private readonly CompositeDisposable _compositeDisposable = new();

        public CameraRule(IInputService inputService, ICameraService cameraService, IBallService ballService)
        {
            _inputService = inputService;
            _cameraService = cameraService;
            _ballService = ballService;
        }

        public async void Initialize()
        {
            await UniTask.WaitUntil(() => _ballService.GetMainBallPositionProvider() != null);
            
            _cameraService.LookAt(_ballService.GetMainBallPositionProvider().Position);
            
            _inputService
                .CursorPositionAsObservable()
                .Subscribe(value =>
                {
                    if (_inputService.TapStatus != TapStatus.OnDrag) return;
                    
                    _cameraService.RotateAroundTarget(value);
                })
                .AddTo(_compositeDisposable);
        }
    }
}