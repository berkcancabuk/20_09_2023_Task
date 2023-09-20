using Assets.Scripts.Model;
using Model.Interface;
using UnityEngine;

namespace Model
{
    public class BarricadeModel : BuildingModel, IBarricade
    {
        private readonly Vector3 _position;

        public BarricadeModel(int health, int attack, Vector3 position) : base(health, attack)
        {
            _position = position;
        }

        public Vector3 Position => _position;
        
        
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