using UnityEngine;
using System.Collections.Generic;
using InputSystem;

public class ControlList : MonoBehaviour
{
    public UIControlItem Prototype;

    private List<UIControlItem> items;

    public void Awake()
    {
        items = new List<UIControlItem>();
        Prototype.gameObject.SetActive(false);
    }

    public void AddControl(ControlData controlData)
    {
        var instance = UIUtility.Add<UIControlItem>(transform, Prototype.gameObject);

        // TODO: Get Icon from InputAction here.
        instance.Label.text = controlData.Label;

        items.Add(instance);
    }

    public void Clear()
    {
        items.ForEach(x => Destroy(x.gameObject));
        items.Clear();
    }
}

public class ControlData
{
    public InputAction Action { get; private set; }
    public string Label { get; private set; }

    public ControlData(InputAction action, string label)
    {
        Action = action;
        Label = label;
    }
}
