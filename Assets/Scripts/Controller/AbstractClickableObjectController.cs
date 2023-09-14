using Assets.Scripts.Model;
using View;
using View.Interface;

namespace Controller
{
    public abstract class AbstractClickableObjectController
    {
        private IClickable _model;
        private IClickableObjectView _view;

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
            if (e.IsTriggered && _model.IsSelected)
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
            }
            else
            {
                _model.IsPositionSetOnce = true;
            }
        }
    }
}