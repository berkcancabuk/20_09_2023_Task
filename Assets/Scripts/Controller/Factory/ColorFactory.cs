using Assets.Scripts.Controller.Exception;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controller.Factory
{
    public enum ColorType
    {
        Selected,
        NotSelected
    }
    public static class ColorFactory
    {
        public static Color GetColor(ColorType colorType)
        {
            switch (colorType)
            {
                case ColorType.Selected:
                    return new Color(16f / 255f, 255f / 255f, 72f / 255f, 87f / 255);
                case ColorType.NotSelected:
                    return new Color(0 / 255f, 0 / 255f, 0 / 255f, 87f / 255);
                default:
                    throw new InvalidColorTypeException("Color type is invalid : " + colorType);
            }
        }
        
    }
}