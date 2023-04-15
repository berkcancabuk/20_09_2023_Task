using System.Collections;
using UnityEngine;
using Assets.Scripts.Model;
using Assets.Scripts.Controller.Exception;
using Vector2 = System.Numerics.Vector2;

namespace Assets.Scripts.Controller.Factory
{
    public class SoldierFactory
    {
        private readonly SoldierLevelOneFactory _soldierLevelOneFactory = new();
        private readonly SoldierLevelTwoFactory _soldierLevelTwoFactory = new();
        private readonly SoldierLevelThreeFactory _soldierLevelThreeFactory = new();

        public ISoldier GenerateSoldier(int level, Vector3 position)
        {
            return level switch
            {
                1 => (SoldierModel)_soldierLevelOneFactory.GenerateWarrior(position),
                2 => (SoldierModel)_soldierLevelTwoFactory.GenerateWarrior(position),
                3 => (SoldierModel)_soldierLevelThreeFactory.GenerateWarrior(position),
                _ => throw new InvalidSoldierLevelException("Invalid Soldier Level : " + level)
            };
        }
    }
}