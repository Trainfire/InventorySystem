using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using Framework;

[RequireComponent(typeof(Button))]
public class UIMenuButton : MonoBehaviour
{
    public event Action<UIMenuButton> Pressed;

    [SerializeField] Text _label;
    [SerializeField] Button _button;

    public string Label
    {
        set { _label.text = value.ToUpper(); }
    }

    public void Awake()
    {
        _button.onClick.AddListener(() => Pressed(this));
    }
}
