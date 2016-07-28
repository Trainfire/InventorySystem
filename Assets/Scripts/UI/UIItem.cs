using UnityEngine;
using System;

namespace UI
{
    public class UIItem : MonoBehaviour
    {
        public event Action<UIItem> Highlighted;
        public event Action<UIItem> Selected;

        public void Select()
        {
            if (Selected != null)
                Selected(this);
        }

        public void Highlight()
        {
            if (Highlighted != null)
                Highlighted(this);
        }
    }
}
