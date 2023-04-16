using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public abstract class BuildingsModel:IBuildings
    {
        public BuildingsModel(int health , int attack)
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

    }
}