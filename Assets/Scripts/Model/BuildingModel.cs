using System;
using Controller;
using Model;
using UnityEditor;
using UnityEngine;
using View.Interface;

namespace Assets.Scripts.Model
{
    public abstract class BuildingModel :IClickable, IBuilding
    {
        private Pointer3D position;
        public BuildingModel(int health , int attack)
        {
            _attack = attack;
            _health = health;
        }
        public enum BuildingType
        {
            SoldierBarrack,
            PowerPlant
        }

        public int _health { get; set; }
        public int _attack { get; set; }
        
        public event EventHandler<BuildingHealthChangedEventArgs> OnHealthChanged = (sender, e) => { };

        public int Health
        {
            get { return _health; }
            set
            {
                if (_health != value)
                {
                    _health = value;

                    var eventArgs = new BuildingHealthChangedEventArgs();
                    OnHealthChanged(this, eventArgs);
                }
            }
        }
        
        public abstract BuildingType GetBuildingType();

        public bool isTriggered { get; set; }
        public bool IsSelected { get; set; }

        public Pointer3D Position {
            get => position;
            set
            {
                // Only if the position changes
                if (!Equals(position, value))
                {
                    // Set new position
                    position = value;
                }
            }
        }

        public bool IsPositionSetOnce { get; set; }
    }
}