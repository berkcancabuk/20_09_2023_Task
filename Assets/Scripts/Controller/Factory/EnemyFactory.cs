using System.Collections;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.Controller.Factory
{
    public class EnemyFactory : IWarriorFactory
    {
        public IWarrior GenerateWarrior(Vector3 position)
        {
            return new EnemyModel(300, 20, position);
        }
    }
}