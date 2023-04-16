using Assets.Scripts.Model;
using Assets.Scripts.View;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.View.Interface;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    public class SoldierBarrackController : IEnemyController
    {
        private readonly ISoldierBarracks _model;
        private readonly ISoldierBarrackView _view;

        public SoldierBarrackController(ISoldierBarracks model, ISoldierBarrackView view)
        {
            _model = model;
            _view = view;
            view.OnClicked += HandleClicked;
            view.OnInstantiate += HandleInstantiate;
            model.OnHealthChanged += HandleHealthChanged;
            model.OnSelectChanged += HandleSelectionChanged;
            model.OnInstantiate += HandleInstantiateChanged;
            SyncPosition();
        }

        private void HandleClicked(object sender, SoldierBarrackClickedEventArgs e)
        {
            _model.IsSelected = true;
        }
        
        private void HandleInstantiate(object sender, SoldierbarrackInstantiateEventArg e)
        {
            _model.IsInstantiate = true;
        }

        private void HandleHealthChanged(object sender, BuildingHealthChangedEventArgs e)
        {
            _view.Health = _model.Health;
        }

        private void HandleSelectionChanged(object sender, SelectedChangedEventArgs e)
        {
            _view.OnClick();
        }
        
        private void HandleInstantiateChanged(object sender, OnInstantiateSpawnPoint e)
        {
            _view.OnInstan();
        }
        private void SyncPosition()
        {
            _view.Position = _model.Position;
        }
    }
}