﻿using Assets.Scripts.Model;
using Model.Interface;
using UnityEngine;

namespace Model
{
    public class PowerPlantModel : BuildingModel, IPowerPlant
    {
        private readonly Vector3 _position;

        public PowerPlantModel(int health, int attack, Vector3 position) : base(health, attack)
        {
            _position = position;
        }

        public Vector3 Position => _position;
        
        
        public override BuildingType GetBuildingType()
        {
            return BuildingType.PowerPlant;
        }
        
    }
}