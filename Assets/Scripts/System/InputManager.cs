using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace InputSystem
{
    // Example list of actions
    public enum InputAction
    {
        None,
        Select,
        Back,
        Interact,
        Inventory,
        Up,
        Right,
        Down,
        Left,
    }

    // The type of action
    public enum InputActionType
    {
        None,
        Down,
        Up,
        Held,
    }

    public interface IInputHandler
    {
        void HandleInput(InputActionEvent action);
    }

    public class InputTest : IInputHandler
    {
        public InputTest(Game game)
        {
            game.InputManager.RegisterHandler(this);
        }

        void IInputHandler.HandleInput(InputActionEvent action)
        {
            Debug.LogFormat("Received InputActionEvent: Action {0}, Type {1}", action.Action, action.Type);
        }
    }

    public class InputActionEvent : EventArgs
    {
        public InputAction Action { get; private set; }
        public InputActionType Type { get; private set; }

        public InputActionEvent(InputAction action, InputActionType type)
        {
            Action = action;
            Type = type;
        }
    }

    // Handles input from an input map and relays to a handler
    public class InputManager
    {
        private List<IInputHandler> handlers;
        private List<InputMap> maps;

        public InputManager()
        {
            handlers = new List<IInputHandler>();
            maps = new List<InputMap>();
        }

        public void RegisterHandler(IInputHandler handler)
        {
            if (!handlers.Contains(handler))
                handlers.Add(handler);
        }

        public void UnregisterHandler(IInputHandler handler)
        {
            if (handlers.Contains(handler))
                handlers.Remove(handler);
        }

        public void RegisterMap(InputMap inputMap)
        {
            if (!maps.Contains(inputMap))
            {
                maps.Add(inputMap);
                inputMap.Trigger += Relay;
            }
        }

        public void UnregisterMap(InputMap inputMap)
        {
            if (maps.Contains(inputMap))
            {
                maps.Remove(inputMap);
                inputMap.Trigger -= Relay;
            }
        }

        void Relay(object sender, InputActionEvent action)
        {
            handlers.ForEach(x => x.HandleInput(action));
        }
    }

    // Maps bindings to an input
    public abstract class InputMap
    {
        public event EventHandler<InputActionEvent> Trigger;

        protected void FireTrigger(InputActionEvent actionEvent)
        {
            if (Trigger != null)
                Trigger(this, actionEvent);
        }
    }

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
        }
    }
}
