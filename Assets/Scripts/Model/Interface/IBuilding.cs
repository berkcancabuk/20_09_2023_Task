using System;

namespace Assets.Scripts.Model
{
    public class BuildingHealthChangedEventArgs : EventArgs
    {}

    public interface IBuilding
    {
        public int Health { get; set; }

        event EventHandler<BuildingHealthChangedEventArgs> OnHealthChanged;
    }
}