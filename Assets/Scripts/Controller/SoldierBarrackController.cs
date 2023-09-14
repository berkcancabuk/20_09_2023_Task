using System;
using System.Threading.Tasks;
using Assets.Scripts.Controller;
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
        private int soldierLevel1Count = 0;
        private int soldierLevel2Count = 0;
        private int soldierLevel3Count = 0;
        private UIManager _uiManager;
        private Pointer3D newlyCreatedSoldierPosition = new Pointer3D(-0.375f, -0.625f, 0f);
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
                await InitializeSoldiers(); // 5 saniyede bir fonksiyonu çağır
                await Task.Delay(5000); // 5 saniye bekle
            }
        }

        async Task InitializeSoldiers()
        {
            soldierLevel1Count+=1;
            soldierLevel2Count+=2;
            soldierLevel3Count+=3;
            _objectView.GenerateSoldier(1,soldierLevel1Count);
            _objectView.GenerateSoldier(2,soldierLevel2Count);
            _objectView.GenerateSoldier(3,soldierLevel3Count);
            if (_uiManager.buildings[0].UI.activeSelf)
            {
                _uiManager.OpenSoldierBarrackPanel(soldierLevel1Count, soldierLevel2Count, soldierLevel3Count);
            }
            await Task.Delay(1000); // Asenkron işlem süresini simüle etmek için bir bekleme ekledik
        }
        private void HandleHealthChanged(object sender, BuildingHealthChangedEventArgs e)
        {
            _objectView.Health = _model.Health;
        }
        
        private void HandleInstantiateChanged(object sender, OnInstantiateSpawnPoint e)
        {
            _objectView.OnInstantiateObj();
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
                _uiManager.OpenSoldierBarrackPanel(soldierLevel1Count, soldierLevel2Count, soldierLevel3Count);
            }
            else
            {
                _model.IsPositionSetOnce = true;
            }
        }
    }
}