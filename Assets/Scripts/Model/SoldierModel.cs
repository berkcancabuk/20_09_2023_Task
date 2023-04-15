using System;
using System.Collections;
using UnityEngine;


namespace Assets.Scripts.Model
{
    public class SoldierModel : Warrior, ISoldier
    {
        private bool _selected = false;

        public SoldierModel(int soldierLevel, int health, int attack, Vector3 position) : base(health, attack)
        {
            _position = position;
            _soldierLevel = soldierLevel;
        }

        public event EventHandler<PositionChangedEventArgs> OnPositionChanged = (sender, e) => { };
        public event EventHandler<SelectedChangedEventArgs> OnSelectChanged = (sender, e) => { };

        private Vector3 _position;

        public Vector3 Position
        {
            get => _position;
            set
            {
                // Only if the position changes
                if (_position != value)
                {
                    // Set new position
                    _position = value;

                    // Dispatch the 'position changed' event
                    var eventArgs = new PositionChangedEventArgs();
                    OnPositionChanged(this, eventArgs);
                }
            }
        }

        private int _soldierLevel { get; set; }

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

        public void berkcan()
        {
            Debug.Log("ahmet");
        }
    }
}