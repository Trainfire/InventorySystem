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
        Equip,
        Drop,
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
        private static List<IInputHandler> handlers;
        private List<InputMap> maps;

        public InputManager()
        {
            handlers = new List<IInputHandler>();
            maps = new List<InputMap>();
        }

        public static void RegisterHandler(IInputHandler handler)
        {
            if (!handlers.Contains(handler))
                handlers.Add(handler);
        }

        public static void UnregisterHandler(IInputHandler handler)
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

    /// <summary>
    /// Adds a repeat behaviour for when a button is held down. When the button is held down and after an initial delay, OnTrigger will be invoked repeatedly with a delay between each call.
    /// </summary>
    public class InputHoldBehaviour : IInputHandler
    {
        public event Action OnTrigger;

        private InputAction trigger;
        private bool buttonDown;
        private float buttonDownTimestamp;
        private float holdRepeatTimestamp;

        private const float HoldActivateDelay = 0.5f;
        private const float HoldRepeatDelay = 0.1f;

        public InputHoldBehaviour(InputAction trigger)
        {
            this.trigger = trigger;

            InputManager.RegisterHandler(this);
        }

        void IInputHandler.HandleInput(InputActionEvent action)
        {
            if (action.Type == InputActionType.Held && action.Action == trigger)
            {
                if (!buttonDown)
                {
                    buttonDown = true;
                    buttonDownTimestamp = Time.realtimeSinceStartup;
                }

                var time = Time.realtimeSinceStartup;

                if (time > buttonDownTimestamp + HoldActivateDelay)
                {
                    if (time > holdRepeatTimestamp + HoldRepeatDelay)
                    {
                        if (OnTrigger != null)
                            OnTrigger();

                        holdRepeatTimestamp = Time.realtimeSinceStartup;
                    }
                }
            }
            else
            {
                buttonDown = false;
            }
        }

        public void Destroy()
        {
            InputManager.UnregisterHandler(this);
            OnTrigger = null;
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
            AddBinding(InputAction.Drop, KeyCode.Delete);
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
