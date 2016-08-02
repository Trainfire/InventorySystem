using UnityEngine;
using System;

namespace Framework
{
    public class TweenFloat : Tween<float>
    {
        protected override float OnTween(float t)
        {
            return Mathf.Lerp(From, To, t);
        }
    }
}
