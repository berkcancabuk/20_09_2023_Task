using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class Building: ISelectable, IMovable
    {
        private Vector3 position;
        public Vector3 Position {
            get { return position; }
            set
            {
                // Only if the position changes
                if (position != value)
                {
                    // Set new position
                    position = value;

                    // Dispatch the 'position changed' event
                    var eventArgs = new PositionChangedEventArgs();
                    OnPositionChanged(this, eventArgs);
                }
            }
        }

        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int _health { get; set; }

        public event EventHandler<PositionChangedEventArgs> OnPositionChanged;
        public event EventHandler<SelectedChangedEventArgs> OnSelectChanged;

        public bool Movable()
        {
            return true;
        }

        public bool Selectable()
        {
            return true;
        }
    }
}