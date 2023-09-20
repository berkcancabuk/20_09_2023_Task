using System.Collections;
using UnityEngine;
using Assets.Scripts.Model;
using Controller;
using Model;

namespace Assets.Scripts.Controller.Factory
{
    public class EnemyFactory : IWarriorFactory
    {
        public IWarrior GenerateWarrior(Pointer3D position)
        {
            return new EnemyModel(300, 20, position);
        }
    }
}