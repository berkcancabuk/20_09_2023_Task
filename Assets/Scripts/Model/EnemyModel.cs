using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class EnemyModel : Warrior, IEnemy
    {
        private readonly Vector3 _position;

        public EnemyModel(int health, int attack, Vector3 position) : base(health, attack)
        {
            _position = position;
        }

        public Vector3 Position => _position;


        public event EventHandler<SelectedChangedEventArgs> OnSelectChanged = (sender, e) => { };
        private bool _selected = false;

        public bool IsSelected
        {
            get => _selected;
            set
            {
                _selected = true;
                var eventArgs = new SelectedChangedEventArgs();
                OnSelectChanged(this, eventArgs);
            }
        }

        public override WarriorType GetWarriorType()
        {
            return WarriorType.Enemy;
        }

        public bool Movable()
        {
            return false;
        }

        public bool Selectable()
        {
            return false;
        }
    }
}