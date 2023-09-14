using Assets.Scripts.Model;
using Assets.Scripts.View;
using System.Collections.Generic;
using Controller;

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

			model.OnHealthChanged += HandleHealthChanged;
			SyncPosition();
		}

		private void HandleClicked(object sender, SoldierClickedEventArgs e)
		{
			model.IsSelected = true;
			view.OnClick();
		}

		private void HandleAttack(object sender, SoldierAttackEventArgs e)
		{
			Pointer3D enemyPosition = e._enemyPosition;
			List<IEnemy> triggeredEnemiesModel = enemies.FindAll(enemy => enemy.Position == enemyPosition);
			triggeredEnemiesModel.ForEach(enemyModel => enemyModel.Health -= model.getAttack());
		}

		private void HandleHealthChanged(object sender, WarriorHealthChangedEventArgs e)
		{
			view.Health = model.Health;
		}

		private void SyncPosition()
		{
			if (model.Position != null)
			{
				view.Position = model.Position.ConvertVector3D();
			}
			else
			{
				model.Position = Pointer3D.ConvertVectorToPointer3D(view.Position);
			}
		}
	}
}