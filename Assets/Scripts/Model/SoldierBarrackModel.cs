using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class SoldierBarrackModel : BuildingsModel, ISoldierBarracks
    {
        private readonly Vector3 _position;

        public SoldierBarrackModel(int health, int attack, Vector3 position) : base(health, attack)
        {
            _position = position;
        }

        public Vector3 Position => _position;
        public event EventHandler<OnInstantiateSpawnPoint> OnInstantiate = (sender, e) => { };


        public event EventHandler<SelectedChangedEventArgs> OnSelectChanged = (sender, e) => { };
        private bool _selected = false;
        private bool _instantiate = false;

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

        public bool IsInstantiate
        {
            get => _instantiate;
            set
            {
                _selected = true;
                var eventArgs = new OnInstantiateSpawnPoint();
                OnInstantiate(this, eventArgs);
            }
        }
        public override BuildingType GetBuildingType()
        {
            return BuildingType.SoldierBarrack;
        }

        public bool Draggable()
        {
            return false;
        }

        public bool Selectable()
        {
            return false;
        }

        
    }
}