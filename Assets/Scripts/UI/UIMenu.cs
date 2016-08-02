using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Framework;
using Framework.UI;
using System;

/// <summary>
/// Base class for a Menu. Also handles Input.
/// </summary>
public class UIMenu : MonoBehaviourEx, IInputHandler
{
    public bool InputEnabled { get; set; }

    private List<IInputHandler> _inputHandlers;

    protected override void OnFirstShow()
    {
        base.OnFirstShow();
        _inputHandlers = new List<IInputHandler>();
        InputManager.RegisterHandler(this);
    }

    protected void RegisterInputHandler(IInputHandler handler)
    {
        _inputHandlers.Add(handler);
    }

    void IInputHandler.HandleInput(InputActionEvent action)
    {
        if (InputEnabled)
            _inputHandlers.ForEach(x => x.HandleInput(action));
    }

    protected virtual void HandleInput(InputActionEvent action) { }
}
