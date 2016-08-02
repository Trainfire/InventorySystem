using System;
using UnityEngine;

namespace Framework.UI
{
    public class UISelectable : MonoBehaviour
    {
        public event Action<UISelectable> Defaulted;
        public event Action<UISelectable> Highlighted;
        public event Action<UISelectable> Selected;

        public bool SelectableByMouse;

        public bool IsSelected { get; private set; }

        public void Default()
        {
            if (Defaulted != null)
                Defaulted.Invoke(this);

            IsSelected = false;

            OnDefault();
        }

        public void Select()
        {
            if (Selected != null)
                Selected.Invoke(this);

            IsSelected = true;

            OnSelect();
        }

        public void Highlight()
        {
            if (Highlighted != null)
                Highlighted.Invoke(this);

            OnHighlight();
        }

        protected virtual void OnDefault() { }
        protected virtual void OnHighlight() { }
        protected virtual void OnSelect() { }
    }
}
