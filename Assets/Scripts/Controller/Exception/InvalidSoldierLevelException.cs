using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controller.Exception
{
    public class InvalidSoldierLevelException : UnityException
    {
        public InvalidSoldierLevelException(string message): base(message) 
        { }
    }
}