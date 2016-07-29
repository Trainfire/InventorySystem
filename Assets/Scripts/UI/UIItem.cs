using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace UI
{
    public class UIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event Action<UIItem> Defaulted;
        public event Action<UIItem> Highlighted;
        public event Action<UIItem> Selected;

        public bool IsSelected { get; private set; }

        public virtual void OnAwake()
        {
            OnDefault();
        }

        public void Default()
        {
            if (Defaulted != null)
                Defaulted(this);

            IsSelected = false;

            OnDefault();
        }

        public void Select()
        {
            if (Selected != null)
                Selected(this);

            IsSelected = true;

            OnSelect();
        }

        public void Highlight()
        {
            if (Highlighted != null)
                Highlighted(this);

            OnHighlight();
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (!IsSelected)
                Default();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (!IsSelected)
                Highlight();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            Select();
        }

        protected virtual void OnDefault() { }
        protected virtual void OnHighlight() { }
        protected virtual void OnSelect() { }
    }

    [RequireComponent(typeof(Button))]
    public class UIItem<TData> : UIItem, IPointerEnterHandler, IPointerClickHandler
    {
        // Ick.
        public event Action<TData> HighlightedData;
        public event Action<TData> SelectedData;

        public TData Data { get; private set; }

        protected override void OnHighlight()
        {
            base.OnHighlight();

            if (HighlightedData != null)
                HighlightedData(Data);
        }

        protected override void OnSelect()
        {
            base.OnSelect();

            if (SelectedData != null)
                SelectedData(Data);
        }

        public void Initialize(TData data)
        {
            OnDefault(); // hack
            Data = data;
        }
    }
}
