using UnityEngine;
using System.Collections;
using System;

namespace Framework
{
    public class TweenVector : Tween<Vector3>
    {
        public Vector3 Value;
        public Vector3 From;
        public Vector3 To;

        protected override Vector3 OnTween(float t)
        {
            return Vector3.Lerp(From, To, t);
        }
    }
}