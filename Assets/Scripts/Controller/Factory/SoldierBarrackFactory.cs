using System.Collections;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.Controller.Factory
{
    public class SoldierBarrackFactory : IBuildingFactory
    {
        public IBuildings GenerateWarrior(Vector3 position)
        {
            return new SoldierBarrackModel(300, 20, position);
        }
    }
}