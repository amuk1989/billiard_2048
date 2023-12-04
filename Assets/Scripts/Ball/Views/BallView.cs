using System;
using Ball.Configs;
using Ball.Controllers;
using Ball.Models;
using Base.Interfaces;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ball.Views
{
    public class BallView : MonoBehaviour, IEntity, IDisposable
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _viewTransform;

        private BallConfigData _configData;
        private BallModel _model;
        private BallHitController _ballHitController;
        private Quaternion _targetRotation = Quaternion.identity;
        private bool _isRotation;

        public string Id => gameObject.GetInstanceID().ToString();

        [Inject]
        private void Construct(BallModel model, BallConfigData configData, BallHitController ballHitController)
        {
            _configData = configData;
            _model = model;
            _ballHitController = ballHitController;
        }

        private void Start()
        {
            transform.position = _model.Position;
            _rigidbody.sleepThreshold = _configData.SleepThreshold;

            _model
                .ForceAsObservable()
                .Subscribe(force => _rigidbody.AddForce(force))
                .AddTo(this);

            _model
                .RotationAsObservable()
                .Subscribe(value =>
                {
                    _targetRotation = value;
                })
                .AddTo(this);
                    

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
            _model.UpdatePosition(transform.position);
            _model.UpdateVelocity(_rigidbody.velocity);

            if (!_isRotation && (_targetRotation.eulerAngles - transform.rotation.eulerAngles).sqrMagnitude > 0.01)
            {
                _isRotation = true;
            }
            else if (_isRotation && (_targetRotation.eulerAngles - transform.rotation.eulerAngles).sqrMagnitude < 0.01)
            {
                _isRotation = false;
            }
            
            if (_isRotation) transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Time.deltaTime * _configData.RotationSpeed);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(!collision.gameObject.TryGetComponent<BallView>(out var collisionBall)) return;
            _ballHitController.OnHit(_model, collisionBall._model);
        }
    }
}
