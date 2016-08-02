using UnityEngine;
using System;

namespace Framework
{
    public class TweenColor : Tween<Color>
    {
        public Color From;
        public Color To;

        protected override Color OnTween(float t)
        {
            return Color.Lerp(From, To, t);
        }
    }
}
