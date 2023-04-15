using UnityEditor;
using UnityEngine;
using System;

namespace Assets.Scripts.Model
{
    public class WarriorHealthChangedEventArgs : EventArgs
    {}

    public interface IWarrior
    {
        public int Health { get; set; }
        public int getAttack();

        event EventHandler<WarriorHealthChangedEventArgs> OnHealthChanged;
    }
}