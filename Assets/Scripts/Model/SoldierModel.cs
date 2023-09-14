using System;
using Controller;
using View.Interface;


namespace Assets.Scripts.Model
{
    public class SoldierModel : Warrior, ISoldier
    {
        private bool _selected = false;

        public SoldierModel(int soldierLevel, int health, int attack, Pointer3D position) : base(health, attack)
        {
            _position = position;
            _soldierLevel = soldierLevel;
        }

        public event EventHandler<SelectedChangedEventArgs> OnSelectChanged = (sender, e) => { };

        private Pointer3D _position;

        public Pointer3D Position
        {
            get => _position;
            set
            {
                // Only if the position changes
                if (_position != value)
                {
                    // Set new position
                    _position = value;

                }
            }
        }

        public bool IsPositionSetOnce { get; set; }

        private int _soldierLevel { get; set; }

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

        public override WarriorType GetWarriorType()
        {
            return WarriorType.Soldier;
        }

        public bool Movable()
        {
            return false;
        }
    }
}