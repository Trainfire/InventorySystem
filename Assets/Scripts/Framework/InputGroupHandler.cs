using UnityEngine;
using System.Collections.Generic;

namespace Framework
{
    public class InputGroupHandler : MonoBehaviour, IInputHandler
    {
        public bool InputEnabled { get; set; }

        private List<IInputHandler> _inputHandlers;

        public void Awake()
        {
            InputManager.RegisterHandler(this);
            _inputHandlers = new List<IInputHandler>();
        }

        public void RegisterInputHandler(IInputHandler handler)
        {
            if (!_inputHandlers.Contains(handler))
                _inputHandlers.Add(handler);
        }

        public void UnregisterInputHandler(IInputHandler handler)
        {
            if (_inputHandlers.Contains(handler))
                _inputHandlers.Remove(handler);
        }

        void IInputHandler.HandleInput(InputActionEvent action)
        {
            if (InputEnabled)
                _inputHandlers.ForEach(x => x.HandleInput(action));
        }
    }
}
