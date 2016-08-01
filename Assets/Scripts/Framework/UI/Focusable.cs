using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System;

namespace Framework.UI
{
    /// <summary>
    /// Marks a GameObject as focusable and triggers appropriate callbacks.
    /// </summary>
    public class Focusable : MonoBehaviour, IPointerEnterHandler
    {
        public UnityEvent OnAwake;
        public UnityEvent Focused;
        public UnityEvent Unfocused;

        public void Awake()
        {
            OnAwake.Invoke();
        }

        public void Focus()
        {
            Focused.Invoke();
        }

        public void Unfocus()
        {
            Unfocused.Invoke();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            Focused.Invoke();
        }
    }
}