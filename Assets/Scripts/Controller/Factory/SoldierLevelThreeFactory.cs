using System.Collections;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.Controller.Factory
{
    public class SoldierLevelThreeFactory : IWarriorFactory
    {
        public IWarrior GenerateWarrior(Vector3 position)
        {
            return new SoldierModel(3, 10, 10, position);
        }
    }
}