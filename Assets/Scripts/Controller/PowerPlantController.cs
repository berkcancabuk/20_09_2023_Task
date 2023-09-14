using Assets.Scripts.Model;
using Assets.Scripts.View;
using Model.Interface;
using View;

namespace Controller
{
    public class PowerPlantController : AbstractClickableObjectController
    {
        private readonly IPowerPlant _model;
        private readonly IPowerPlantView _view;

        public PowerPlantController(IPowerPlant model, IPowerPlantView view) : base(model, view)
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