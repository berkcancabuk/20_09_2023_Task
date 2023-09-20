using System;
using Controller;
using UnityEngine;
using Color = UnityEngine.Color;

namespace View.Interface
{
    
    public class TriggeredEventArg
    {
        public bool IsTriggered {
            get;
            set;
        }

    }
    
    public class MouseMoveEventArg
    {
        public  Pointer3D point {
            get;
            set;
        }

    }
    public interface IClickableObjectView
    {
        public bool isTriggered { get; set; }
        public Color DefaultColor { get; set; }
        event EventHandler<TriggeredEventArg> OnTriggeredEvent;
        event EventHandler<MouseMoveEventArg> OnMouseDragEvent;
        void SetToDefaultColor();
        
        void ChangeColor(Color color);
        void ChangePosition(Pointer3D pointer);
        
        event EventHandler<OnClickedEventArgs> OnClicked;
        void OnClick();
        
    }
}