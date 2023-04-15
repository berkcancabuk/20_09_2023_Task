using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controller.Exception
{
    public class InvalidColorTypeException : UnityException
    {
        public InvalidColorTypeException(string message): base(message) 
        { }
    }
}