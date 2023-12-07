using System;
using Cue.Models;
using UnityEngine;
using Zenject;
using UniRx;

namespace Cue.Views
{
    internal class CueView : MonoBehaviour, IDisposable
    {
        internal class Factory: PlaceholderFactory<CueModel, CueView>
        {
        }
        
        private CueModel _model;
        private Vector3 _target;
        
        [Inject]
        private void Construct(CueModel model)
        {
            _model = model;
        }

        private void Start()
        {
            _model
                .TargetAsObservable()
                .Subscribe(value => _target = value)
                .AddTo(this);
            
            _model
                .PositionAsObservable()
                .Subscribe(value =>
                {
                    transform.position = value;
                    transform.LookAt(_target);
                })
                .AddTo(this);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}