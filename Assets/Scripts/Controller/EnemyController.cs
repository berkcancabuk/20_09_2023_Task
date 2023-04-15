using Assets.Scripts.Model;
using Assets.Scripts.View;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.View.Interface;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    public class EnemyController : IEnemyController
    {
        private readonly IEnemy _model;
        private readonly IEnemyView _view;
        private readonly List<ISoldier> _soldiers;

        public EnemyController(IEnemy model, IEnemyView view, List<ISoldier> soldiers)
        {
            _model = model;
            _view = view;
            _soldiers = soldiers;
            view.OnClicked += HandleClicked;
            view.OnAttack += HandleAttack;

            model.OnHealthChanged += HandleHealthChanged;
            model.OnSelectChanged += HandleSelectionChanged;
            SyncPosition();
        }

        private void HandleClicked(object sender, EnemyClickedEventArgs e)
        {
            _model.IsSelected = true;
        }

        private void HandleAttack(object sender, EnemyAttackEventArgs e)
        {
            var soldiersPosition = e.SoldierPosition;
            var triggeredSoldierModel = _soldiers.FindAll(soldiers => soldiers.Position == soldiersPosition);
            triggeredSoldierModel.ForEach(soldierModel => soldierModel.Health -= _model.getAttack());
        }


        private void HandleHealthChanged(object sender, WarriorHealthChangedEventArgs e)
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