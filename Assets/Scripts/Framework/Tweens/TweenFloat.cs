using UnityEngine;
using System;

namespace Framework
{
    public class TweenFloat : Tween<float>
    {
        public float Value;
        public float From;
        public float To;

        protected override float OnTween(float t)
        {
            return Mathf.Lerp(From, To, t);
        }
    }
}