using UnityEngine;
using System.Collections;
using System;

namespace Framework
{
    public class TweenQuat : Tween<Quaternion>
    {
        protected override Quaternion OnTween(float t)
        {
            return Quaternion.Lerp(From, To, t);
        }
    }
}
