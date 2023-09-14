using System;
using Controller;
using View.Interface;

namespace Assets.Scripts.Model
{

    public class SelectedChangedEventArgs : EventArgs
    { }
    public interface IClickable
    {
        public bool isTriggered { get; set; }
        public bool IsSelected { get; set; }
        
        Pointer3D Position { get; set; }

        public bool IsPositionSetOnce { get; set; }
    }
}