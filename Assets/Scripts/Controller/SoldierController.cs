using Assets.Scripts.Model;
using Assets.Scripts.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controller
{
	public interface ISoldierController
	{
	}

	public class SoldierController : ISoldierController
	{
		private readonly ISoldier model;
		private readonly ISoldierView view;
		private readonly List<IEnemy> enemies = new();
		
		public SoldierController(ISoldier model, ISoldierView view, List<IEnemy> enemies)
		{
			this.model = model;
			this.view = view;
			this.enemies = enemies;
			view.OnClicked += HandleClicked;
			view.OnAttack += HandleAttack;

			model.OnPositionChanged += HandlePositionChanged;
			model.OnHealthChanged += HandleHealthChanged;
			model.OnSelectChanged += HandleSelectionChanged;
			SyncPosition();
		}

		private void HandleClicked(object sender, SoldierClickedEventArgs e)
		{
			model.IsSelected = true;
		}

		private void HandleAttack(object sender, SoldierAttackEventArgs e)
		{
			Vector3 enemyPosition = e._enemyPosition.Value;
			List<IEnemy> triggeredEnemiesModel = enemies.FindAll(enemy => enemy.Position == enemyPosition);
			triggeredEnemiesModel.ForEach(enemyModel => enemyModel.Health -= model.getAttack());
		}

		private void HandlePositionChanged(object sender, PositionChangedEventArgs e)
		{
			SyncPosition();
		}

		private void HandleHealthChanged(object sender, WarriorHealthChangedEventArgs e)
		{
			view.Health = model.Health;
		}

		private void HandleSelectionChanged(object sender, SelectedChangedEventArgs e)
		{
			view.OnClick();
		}

		private void SyncPosition()
		{
			view.Position = model.Position;
		}
	}
}