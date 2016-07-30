using UnityEngine;
using System.Collections;
using System;

public class TweenQuat : Tween<Quaternion>
{
    public Quaternion Value;
    public Quaternion From;
    public Quaternion To;

    protected override Quaternion OnTween(float t)
    {
        return Quaternion.Lerp(From, To, t);
    }
}
