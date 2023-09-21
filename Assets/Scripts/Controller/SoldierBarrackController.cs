using System.Threading.Tasks;
using Assets.Scripts.Model;
using Model.Interface;
using UnityEngine;
using View;

namespace Controller
{
    public class SoldierBarrackController: AbstractClickableObjectController 
    {
        private readonly ISoldierBarracks _model;
        private readonly ISoldierBarrackObjectView _objectView;
        private UIManager _uiManager;
        public SoldierBarrackController(ISoldierBarracks model, ISoldierBarrackObjectView objectView, UIManager uiManager) : base(model, objectView)
        {
            _model = model;
            _objectView = objectView;
            _uiManager = uiManager;
            objectView.OnInstantiate += HandleInstantiate;
            model.OnHealthChanged += HandleHealthChanged;
            _objectView.OnClicked += HandleClicked;
            SyncPosition();
        }
        
        private async void HandleInstantiate(object sender, SoldierbarrackInstantiateEventArg e)
        {
            Debug.Log("Initializing soldier barrack ");
            _model.IsInstantiate = true;
            while (true)
            {
                await InitializeSoldiers(); // 2 saniyede bir fonksiyonu çağır
                await Task.Delay(1000); // 2 saniye bekle
            }
        }

        async Task InitializeSoldiers()
        {
            _objectView.Soldier1Count += 1;
            _objectView.Soldier2Count += 2;
            _objectView.Soldier3Count += 3;
            _objectView.GenerateSoldier(1,_objectView.Soldier1Count);
            _objectView.GenerateSoldier(2,_objectView.Soldier2Count);
            _objectView.GenerateSoldier(3,_objectView.Soldier3Count);
            // if (_uiManager.buildings[0].UI.activeSelf)
            // {
            //     _uiManager.OpenSoldierBarrackPanel(_objectView.Soldier1Count, _objectView.Soldier2Count, _objectView.Soldier3Count);
            // }
            await Task.Delay(1000); // Asenkron işlem süresini simüle etmek için bir bekleme ekledik
        }
        private void HandleHealthChanged(object sender, BuildingHealthChangedEventArgs e)
        {
            _objectView.Health = _model.Health;
        }
        private void SyncPosition()
        {
            if (_model.Position != null)
            {
                _objectView.Position = _model.Position.ConvertVector3D();
            }
            else
            {
                _model.Position = Pointer3D.ConvertVectorToPointer3D(_objectView.Position);
            }
        }
        private void HandleClicked(object sender, OnClickedEventArgs e)
        {
            _model.IsSelected = e.IsClicked;
            if (e.IsClicked)
            {
                _uiManager.OpenSoldierBarrackPanel(_objectView.Soldier1Count, _objectView.Soldier2Count, _objectView.Soldier3Count);
            }
        }
    }
}