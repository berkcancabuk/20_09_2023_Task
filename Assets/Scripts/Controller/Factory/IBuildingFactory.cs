using System.Collections;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.Controller.Factory
{
    public interface IBuildingFactory
    {
        public IBuildings GenerateWarrior(Vector3 position);
    }
}