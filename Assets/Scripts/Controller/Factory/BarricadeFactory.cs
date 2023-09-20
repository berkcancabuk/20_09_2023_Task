using Assets.Scripts.Controller.Factory;
using Assets.Scripts.Model;
using Model;
using UnityEngine;

namespace Controller.Factory
{
    public class BarricadeFactory : IBuildingFactory
    {
        public IBuilding GenerateBuilding(Vector3 position)
        {
            return new BarricadeModel(300, 20, position);
        }
    }
}