using System;
using Assets.Scripts.Model;
using Controller;

namespace Model
{
    public class EnemyModel : Warrior, IEnemy
    {
        private readonly Pointer3D _position;

        public EnemyModel(int health, int attack, Pointer3D position) : base(health, attack)
        {
            _position = position;
        }

        public event EventHandler<SelectedChangedEventArgs> OnSelectChanged = (sender, e) => { };
        private bool _selected = false;

        public bool isTriggered { get; set; }

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

        public Pointer3D Position { get; set; }
        public bool IsPositionSetOnce { get; set; }

        public override WarriorType GetWarriorType()
        {
            return WarriorType.Enemy;
        }
    }
}