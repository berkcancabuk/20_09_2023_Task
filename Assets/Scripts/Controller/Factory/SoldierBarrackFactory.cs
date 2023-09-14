using System.Collections;
using UnityEngine;
using Assets.Scripts.Model;
using Model;

namespace Assets.Scripts.Controller.Factory
{
    public class SoldierBarrackFactory : IBuildingFactory
    {
        public IBuilding GenerateBuilding(Vector3 position)
        {
            return new SoldierBarrackModel(300, 20, position);
        }
    }
}