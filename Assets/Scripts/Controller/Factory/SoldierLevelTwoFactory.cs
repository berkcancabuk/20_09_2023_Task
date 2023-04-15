using Assets.Scripts.Model;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controller.Factory
{
    public class SoldierLevelTwoFactory : IWarriorFactory
    {
        public IWarrior GenerateWarrior(Vector3 position)
        {
            return new SoldierModel(2, 10, 5, position);
        }
    }
}