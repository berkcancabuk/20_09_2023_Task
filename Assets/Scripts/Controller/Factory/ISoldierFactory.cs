using System.Collections;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.Controller.Factory
{
    public interface ISoldierFactory
    {
        public ISoldier GenerateSoldier(int level);


    }
}