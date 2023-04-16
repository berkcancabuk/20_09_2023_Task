using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class PowerPlantModel : BuildingsModel, IPowerPlant
    {
        private readonly Vector3 _position;

        public PowerPlantModel(int health, int attack, Vector3 position) : base(health, attack)
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
        public override BuildingType GetBuildingType()
        {
            return BuildingType.PowerPlant;
        }

        
        public bool Draggable()
        {
            return true;
        }

        public bool Selectable()
        {
            return false;
        }

        
    }
}