using System;
using Ball.Configs;
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

        public string Id => gameObject.GetInstanceID().ToString();

        [Inject]
        private void Construct(BallModel model, BallConfigData configData)
        {
            _configData = configData;
            _model = model;
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
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"[BallView] Hit to {collision.gameObject.name}");
            
            if(!collision.gameObject.TryGetComponent<BallView>(out var collisionBall)) return;

            Debug.Log($"[BallView] {collisionBall.gameObject.name} is BallView");
            
            _model.OnHit(collisionBall._model);
        }
    }
}
