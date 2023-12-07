using Base.Interfaces;
using Cue.Interfaces;
using Cue.Models;
using Cue.Views;
using UnityEngine;

namespace Cue.Services
{
    internal class CueService: ICueService
    {
        private readonly CueModel _model;
        private readonly CueView.Factory _factory;
        
        private CueView _view;

        public CueService(CueModel model, CueView.Factory factory)
        {
            _model = model;
            _factory = factory;
        }

        public void Activate()
        {
            _view = _factory.Create(_model);
        }

        public void Deactivate()
        {
            if (_view != null) _view.Dispose();
        }

        public void SetTarget(Vector3 target)
        {
            _model.SetTarget(target);
        }

        public void SetHandler(IPositionProvider handler)
        {
            _model.SetPositionHandler(handler);
        }
    }
}