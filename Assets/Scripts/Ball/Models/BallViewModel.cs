using System;
using Ball.Configs;
using Base.Interfaces;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Ball.Models
{
    internal class BallViewModel: IInitializable
    {
        public class Factory: PlaceholderFactory<BallModel, BallViewModel>
        {
        }
        
        private readonly IPositionProvider _targetProvider;
        private readonly BallConfigData _configData;
        
        private readonly ReactiveProperty<Vector3> _position = new();
        private readonly ReactiveProperty<Quaternion> _rotation = new();

        private readonly CompositeDisposable _compositeDisposable = new();
        
        private Quaternion _viewRotation;
        private bool _isRotation = false;

        public BallViewModel(BallModel ballModel, BallConfigData configData)
        {
            _targetProvider = ballModel.TargetProvider;
            _configData = configData;
        }

        public IObservable<Quaternion> RotationAsObservable() => _rotation.AsObservable();

        public void Initialize()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    var targetRotation = Quaternion.LookRotation(_targetProvider.Position - _position.Value);

                    _isRotation = _isRotation switch
                    {
                        //TODO: need to find some kind of comparison logic.
                        false when (targetRotation.eulerAngles - _viewRotation.eulerAngles).sqrMagnitude > 0.01 => true,
                        true when (targetRotation.eulerAngles - _viewRotation.eulerAngles).sqrMagnitude < 0.01 => false,
                        _ => _isRotation
                    };
                    
                    if (!_isRotation) return;

                    _rotation.Value = Quaternion.Lerp(_viewRotation, targetRotation, Time.deltaTime * _configData.RotationSpeed);
                })
                .AddTo(_compositeDisposable);
        }

        public void UpdatePosition(Vector3 position)
        {
            _position.Value = position;
        }
        
        public void UpdateRotation(Quaternion rotation)
        {
            _viewRotation = rotation;
        }
    }
}