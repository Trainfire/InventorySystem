using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class InteractableObjectEvent : UnityEvent<InteractableObject> { }

public class InteractableObject : MonoBehaviour
{
    public InteractableObjectEvent LookEntered;
    public InteractableObjectEvent LookLeft;

    public void LookEnter()
    {
        LookEntered.Invoke(this);
    }

    public void LookLeave()
    {
        LookLeft.Invoke(this);
    }
}
