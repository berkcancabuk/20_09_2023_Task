using System;
using Controller;
using UnityEngine;
using View.Interface;

namespace View
{
    public interface IPowerPlantView : IClickableObjectView
    {
        Vector3 Position { get; set; }
        int Health { get; set; }

    }

    public class PowerPlantView : MovableObjectView, IPowerPlantView
    {
        

        public Vector3 position;

        public Vector3 Position
        {
            get => position;
            set
            {
                position = value;
                transform.position = value;
            }
        }

        public void ChangePosition(Pointer3D pointer)
        {
            Position = pointer.ConvertVector3D();
        }

        public void OnClick()
        {
            UIManager.instance.OpenPowerPlantPanel();
        }
        

    }
}