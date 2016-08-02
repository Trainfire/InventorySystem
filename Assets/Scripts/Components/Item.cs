using UnityEngine;
using UnityEngine.Events;
using System;
using Models;

[Serializable]
class ItemPickedEvent : UnityEvent<Item> { }

class Item : MonoBehaviour
{
    public ItemPickedEvent PickedUp;

    public ItemData ItemData;

    public void Awake()
    {
        PickedUp = new ItemPickedEvent();
    }

    public void Pickup()
    {
        if (PickedUp != null)
            PickedUp.Invoke(this);
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        PickedUp.RemoveAllListeners();
    }
}
