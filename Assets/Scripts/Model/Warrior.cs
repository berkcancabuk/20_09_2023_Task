using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public abstract class Warrior:IWarrior
    {
        public Warrior(int health , int attack)
        {
            _attack = attack;
            _health = health;
        }
        public enum WarriorType
        {
            Soldier,
            Enemy
        }

        public int _health { get; set; }
        public int _attack { get; set; }
        
        public event EventHandler<WarriorHealthChangedEventArgs> OnHealthChanged = (sender, e) => { };

        public int Health
        {
            get { return _health; }
            set
            {
                if (_health != value)
                {
                    _health = value;

                    var eventArgs = new WarriorHealthChangedEventArgs();
                    OnHealthChanged(this, eventArgs);
                }
            }
        }


        public abstract WarriorType GetWarriorType();

        public int getAttack()
        {
            return _attack;
        }
    }
}