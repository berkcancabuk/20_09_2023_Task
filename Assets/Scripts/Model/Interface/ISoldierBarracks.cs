using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class OnInstantiateSpawnPoint
    {
    }
    public interface ISoldierBarracks: IBuildings,ISelectable
    {
        Vector3 Position { get; }
        public bool IsInstantiate { get; set; }
        event EventHandler<OnInstantiateSpawnPoint> OnInstantiate;
    }

   
}