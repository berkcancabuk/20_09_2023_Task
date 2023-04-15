using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public interface IEnemy: IWarrior,ISelectable
    {
        Vector3 Position { get; }
    }
}