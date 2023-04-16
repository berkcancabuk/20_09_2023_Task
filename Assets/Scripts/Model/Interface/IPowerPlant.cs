using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Model
{
   
    public interface IPowerPlant: IBuildings,ISelectable
    {
        Vector3 Position { get; }
    }

   
}