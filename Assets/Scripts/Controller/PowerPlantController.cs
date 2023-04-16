using Assets.Scripts.Model;
using Assets.Scripts.View;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.View.Interface;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    public class PowerPlantController : IEnemyController
    {
        private readonly IPowerPlant _model;
        private readonly IPowerPlantView _view;

        public PowerPlantController(IPowerPlant model, IPowerPlantView view)
        {
            _model = model;
            _view = view;
            view.OnClicked += HandleClicked;
            model.OnHealthChanged += HandleHealthChanged;
            model.OnSelectChanged += HandleSelectionChanged;
            SyncPosition();
        }

        private void HandleClicked(object sender, SoldierBarrackClickedEventArgs e)
        {
            _model.IsSelected = true;
        }

        private void HandleHealthChanged(object sender, BuildingHealthChangedEventArgs e)
        {
            _view.Health = _model.Health;
        }

        private void HandleSelectionChanged(object sender, SelectedChangedEventArgs e)
        {
            _view.OnClick();
        }
        
        private void SyncPosition()
        {
            _view.Position = _model.Position;
        }
    }
}