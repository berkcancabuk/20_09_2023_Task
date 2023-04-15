using System.Collections;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.Controller.Factory
{
    public interface IWarriorFactory
    {
        public IWarrior GenerateWarrior(Vector3 position);
    }
}