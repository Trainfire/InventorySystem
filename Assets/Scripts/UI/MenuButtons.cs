using UnityEngine;
using System;
using System.Collections.Generic;
using Framework.UI;

class MenuButtons : MonoBehaviour
{
    public event Action<MenuBase> ButtonPressed;

    [SerializeField] private UIMenuButton Prototype;

    private Dictionary<UIMenuButton, MenuBase> _buttons;

    public void Awake()
    {
        _buttons = new Dictionary<UIMenuButton, MenuBase>();

        Prototype.gameObject.SetActive(false);
    }

    public void AddButton(MenuBase menu, string label)
    {
        var instance = UIUtility.Add<UIMenuButton>(transform, Prototype.gameObject);
        instance.Label = label;
        instance.Pressed += Button_Pressed;

        _buttons.Add(instance, menu);
    }

    private void Button_Pressed(UIMenuButton obj)
    {
        if (ButtonPressed != null)
            ButtonPressed(_buttons[obj]);
    }
}
