using Assets.Scripts.Model;
using Model.Interface;
using View;

namespace Controller
{
    public class BarricadeController : AbstractClickableObjectController
    {
        private readonly IBarricade _model;
        private readonly IBarricadeView _view;

        public BarricadeController(IBarricade model, IBarricadeView view) : base(model, view)
        {
            _model = model;
            _view = view;
            model.OnHealthChanged += HandleHealthChanged;
            SyncPosition();
        }
        
        private void HandleHealthChanged(object sender, BuildingHealthChangedEventArgs e)
        {
            _view.Health = _model.Health;
        }
        
        private void SyncPosition()
        {
            if (_model.Position != null)
            {
                _view.Position = _model.Position.ConvertVector3D();
            }
            else
            {
                _model.Position = Pointer3D.ConvertVectorToPointer3D(_view.Position);
            }
        }
    }
}