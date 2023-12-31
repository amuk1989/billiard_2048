using System;
using Ball.Configs;
using Ball.Data;
using Ball.Models;
using Ball.Utilities;
using Base.Interfaces;
using UniRx;
using UnityEngine;
using Utility;
using Zenject;

namespace Ball.Views
{
    public class BallView : MonoBehaviour, IEntity, IDisposable
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _viewTransform;
        [SerializeField] private Renderer _renderer;

        private BallConfigData _configData;
        private BallModel _model;
        private BallViewModel _ballViewModel;
        private BallHitUtility _ballHitUtility;
        
        public string Id => gameObject.GetInstanceID().ToString();

        [Inject]
        private void Construct(BallViewData data, BallConfigData configData, BallHitUtility ballHitUtility)
        {
            _configData = configData;
            _model = data.BallModel;
            _ballViewModel = data.BallViewModel;
            _ballHitUtility = ballHitUtility;
        }

        private void Start()
        {
            transform.position = _model.Position;
            _rigidbody.sleepThreshold = _configData.SleepThreshold;

            _model
                .ForceAsObservable()
                .Subscribe(force => _rigidbody.AddForce(force))
                .AddTo(this);

            _ballViewModel
                .RotationAsObservable()
                .Subscribe(value => _viewTransform.rotation = value)
                .AddTo(this);

            _model
                .HitPointsAsObservable()
                .Subscribe(value =>
                {
                    foreach (var hitPointsPresentData in _configData.HitPointsData)
                    {
                        if (hitPointsPresentData.HitPoints != value) continue;
                        _renderer.material.SetColor(Consts.BallColor, hitPointsPresentData.Color);
                    }
                    _renderer.material.SetInt(Consts.BallShaderValue, (int) value);
                });
                    

#if UNITY_EDITOR
            gameObject.name += $"_{Id}";
#endif
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            _ballViewModel.UpdatePosition(transform.position);
            _ballViewModel.UpdateRotation(_viewTransform.rotation);
            
            _model.UpdateVelocity(_rigidbody.velocity);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(!collision.gameObject.TryGetComponent<BallView>(out var collisionBall)) return;
            _ballHitUtility.OnHit(_model, collisionBall._model);
        }
    }
}
