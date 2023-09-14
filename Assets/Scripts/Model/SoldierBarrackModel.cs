using System;
using Assets.Scripts.Model;
using Model.Interface;
using UnityEngine;

namespace Model
{
    public class SoldierBarrackModel : BuildingModel, ISoldierBarracks
    {
        private readonly Vector3 _position;
        private bool _isTriggered { get; set; }
        public SoldierBarrackModel(int health, int attack, Vector3 position) : base(health, attack)
        {
            _position = position;
        }

        public Vector3 Position => _position;


        public event EventHandler<SelectedChangedEventArgs> OnSelectChanged = (sender, e) => { };
        

        public bool IsInstantiate { get; set; }
        
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