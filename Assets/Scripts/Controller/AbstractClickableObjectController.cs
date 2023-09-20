using Assets.Scripts.Model;
using UnityEngine;
using View;
using View.Interface;

namespace Controller
{
    public abstract class AbstractClickableObjectController
    {
        private IClickable _model;
        private IClickableObjectView _view;
        private Pointer3D _firstPosition;
        protected AbstractClickableObjectController(IClickable model,IClickableObjectView view)
        {
            _model = model;
            _view = view;
            view.OnTriggeredEvent += HandleTriggerEventChange;
            view.OnMouseDragEvent += HandleMouseMoveEventChange;
            view.OnClicked += HandleClicked;
        }

        private void HandleTriggerEventChange(object sender, TriggeredEventArg e)
        {
            _model.isTriggered = e.IsTriggered;
            if (e.IsTriggered && _model.IsSelected && !_model.IsPositionSetOnce)
            {
                _view.ChangeColor( UnityEngine.Color.red);
            }
            else
            {
                _view.SetToDefaultColor();
            }
            
        }
        
        
        private void HandleMouseMoveEventChange(object sender, MouseMoveEventArg e)
        {
            if (!_model.IsPositionSetOnce)
            {
                _model.Position = e.point;
                _view.ChangePosition(e.point);
            }
        }
        
        private void HandleClicked(object sender, OnClickedEventArgs e)
        {
            _model.IsSelected = e.IsClicked;
            if (e.IsClicked)
            {
                _view.OnClick();
                _firstPosition = _model.Position;
            }
            else
            {
                if (_model.isTriggered)
                {
                    _model.Position = _firstPosition;
                    _view.ChangePosition(_firstPosition);
                    _model.IsPositionSetOnce = false;
                    return;
                }
                _model.IsPositionSetOnce = true;
            }
        }
    }
}