using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Framework;

// Example of an input map for PC.
public class InputMapPC : InputMap, IMonoLateUpdateReceiver
{
    Dictionary<InputAction, KeyCode> bindings;

    public InputMapPC()
    {
        MonoEventRelay.RegisterForLateUpdate(this);
        bindings = new Dictionary<InputAction, KeyCode>();

        // Default bindings
        AddBinding(InputAction.Select, KeyCode.Space);
        AddBinding(InputAction.Inventory, KeyCode.Tab);
        AddBinding(InputAction.Back, KeyCode.Backspace);
        AddBinding(InputAction.Interact, KeyCode.E);
        AddBinding(InputAction.Up, KeyCode.UpArrow);
        AddBinding(InputAction.Right, KeyCode.RightArrow);
        AddBinding(InputAction.Down, KeyCode.DownArrow);
        AddBinding(InputAction.Left, KeyCode.LeftArrow);
        AddBinding(InputAction.Drop, KeyCode.Delete);
        AddBinding(InputAction.LeftBumper, KeyCode.Q); // Debug
        AddBinding(InputAction.RightBumper, KeyCode.E); // Debug
    }

    public void AddBinding(InputAction action, KeyCode key)
    {
        if (bindings.ContainsKey(action))
        {
            Debug.LogErrorFormat("InputMapPC: '{0}' is already bound to '{1}'", action, key);
        }
        else
        {
            bindings.Add(action, key);
        }
    }

    void IMonoLateUpdateReceiver.OnLateUpdate()
    {
        foreach (var kvp in bindings)
        {
            if (Input.anyKey)
            {
                if (Input.GetKeyDown(kvp.Value))
                {
                    var e = new InputActionEvent(kvp.Key, InputActionType.Down);
                    FireTrigger(e);
                }

                if (Input.GetKey(kvp.Value))
                {
                    var e = new InputActionEvent(kvp.Key, InputActionType.Held);
                    FireTrigger(e);
                }
            }

            if (Input.GetKeyUp(kvp.Value))
            {
                var e = new InputActionEvent(kvp.Key, InputActionType.Up);
                FireTrigger(e);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            FireTrigger(new InputActionEvent(InputAction.ScrollUp, InputActionType.Down));
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            FireTrigger(new InputActionEvent(InputAction.ScrollDown, InputActionType.Down));
        }

        if (Input.GetMouseButtonDown(0))
            FireTrigger(new InputActionEvent(InputAction.MouseLeft, InputActionType.Down));

        if (Input.GetMouseButtonDown(1))
            FireTrigger(new InputActionEvent(InputAction.MouseRight, InputActionType.Down));
    }
}
