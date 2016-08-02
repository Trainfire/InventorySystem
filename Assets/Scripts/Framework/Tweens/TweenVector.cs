using UnityEngine;
using System.Collections;
using System;

namespace Framework
{
    public class TweenVector : Tween<Vector3>
    {
        protected override Vector3 OnTween(float t)
        {
            return Vector3.Lerp(From, To, t);
        }
    }
}
