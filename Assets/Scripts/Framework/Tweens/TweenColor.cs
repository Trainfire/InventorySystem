using UnityEngine;
using System;

namespace Framework
{
    public class TweenColor : Tween<Color>
    {
        protected override Color OnTween(float t)
        {
            return Color.Lerp(From, To, t);
        }
    }
}
