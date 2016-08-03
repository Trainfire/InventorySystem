using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Framework.UI;

class MenuButtons : MonoBehaviour
{
    public event Action<MenuBase> ButtonPressed;

    [SerializeField] private UIMenuButton Prototype;

    private Dictionary<UIMenuButton, MenuBase> _buttons;
    private UIMenuButton _current;

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

        if (_current == null)
            _current = instance;

        _buttons.Add(instance, menu);
    }

    public void Select(MenuBase menu)
    {
        if (_buttons.ContainsValue(menu))
        {
            if (_current != null)
                _current.Selected(false);
            _current = _buttons.FirstOrDefault(x => x.Value == menu).Key;
            _current.Selected(true);
        }
    }

    public void Update()
    {
        if (_current != null)
        {
            _current.Button.Select();
        }
    }

    private void Button_Pressed(UIMenuButton button)
    {
        if (ButtonPressed != null)
            ButtonPressed(_buttons[button]);

        _current = button;
    }
}
