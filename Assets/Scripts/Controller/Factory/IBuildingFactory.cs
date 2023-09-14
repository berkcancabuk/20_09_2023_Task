using System.Collections;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.Controller.Factory
{
    public interface IBuildingFactory
    {
        public IBuilding GenerateBuilding(Vector3 position);
    }
}