using System.Collections;
using UnityEngine;
using System;

namespace Assets.Scripts.Model
{

    public class PositionChangedEventArgs : EventArgs
    { }

    public interface IMovable
    {
        Vector3 Position { get; set; }

        public bool Movable();


        event EventHandler<PositionChangedEventArgs> OnPositionChanged;


    }
}