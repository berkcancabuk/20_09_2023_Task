using System;
using Controller;
using View.Interface;

namespace Assets.Scripts.Model
{
    public class Building: IClickable
    {
        private Pointer3D position;
        public Pointer3D Position {
            get => position;
            set
            {
                // Only if the position changes
                if (!Equals(position, value))
                {
                    // Set new position
                    position = value;

                }
            }
        }

        public bool IsPositionSetOnce { get; set; }

        public bool isTriggered { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int _health { get; set; }
        
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