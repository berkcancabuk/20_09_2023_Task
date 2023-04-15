using Assets.Scripts.Model;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controller.Factory
{
    public class SoldierLevelOneFactory : IWarriorFactory
    {
        public IWarrior GenerateWarrior(Vector3 position)
        {
            return new SoldierModel(1, 10, 2, position);
        }
    }
}