using System.Collections;
using UnityEngine;
using System;

namespace Assets.Scripts.Model
{

    public class SelectedChangedEventArgs : EventArgs
    { }
    public interface ISelectable
    {
        public bool IsSelected { get; set; }

        event EventHandler<SelectedChangedEventArgs> OnSelectChanged;

    }
}