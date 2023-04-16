using UnityEditor;
using UnityEngine;
using System;

namespace Assets.Scripts.Model
{
    public class BuildingHealthChangedEventArgs : EventArgs
    {}

    public interface IBuildings
    {
        public int Health { get; set; }

        event EventHandler<BuildingHealthChangedEventArgs> OnHealthChanged;
    }
}