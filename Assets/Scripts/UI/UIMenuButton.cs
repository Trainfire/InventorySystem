using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using Framework;

[RequireComponent(typeof(Button))]
public class UIMenuButton : MonoBehaviour
{
    public event Action<UIMenuButton> Pressed;

    [SerializeField] private Text _label;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _underline;

    public Button Button
    {
        get { return _button; }
    }

    public string Label
    {
        set { _label.text = value.ToUpper(); }
    }

    public void Awake()
    {
        _button.onClick.AddListener(OnPress);
        _underline.SetActive(false);
    }

    public void Selected(bool selected)
    {
        _underline.SetActive(selected);
    }

    private void OnPress()
    {
        if (Pressed != null)
            Pressed.Invoke(this);
    }
}
