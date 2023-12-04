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

        private BallConfigData _configData;
        private BallModel _model;
        private BallHitController _ballHitController;

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
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"[BallView] Hit to {collision.gameObject.name}");
            
            if(!collision.gameObject.TryGetComponent<BallView>(out var collisionBall)) return;

            Debug.Log($"[BallView] {collisionBall.gameObject.name} is BallView");
            
            _ballHitController.OnHit(_model, collisionBall._model);
        }
    }
}
