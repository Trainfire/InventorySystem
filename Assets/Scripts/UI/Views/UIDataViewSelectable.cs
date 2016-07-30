using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace UI
{
    public class UIDataViewSelectable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event Action<UIDataViewSelectable> Removed;
        public event Action<UIDataViewSelectable> Defaulted;
        public event Action<UIDataViewSelectable> Highlighted;
        public event Action<UIDataViewSelectable> Selected;
        public event Action<DataViewSelectableEvent> StateChanged;

        public class DataViewSelectableEvent : EventArgs
        {
            public State State { get; private set; }
            public UIDataViewSelectable View { get; private set; }
            
            public DataViewSelectableEvent(UIDataViewSelectable view, State state)
            {
                View = view;
                State = state;
            }
        }

        public enum State
        {
            Default,
            Highlighted,
            Selected,
        }

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

        public void Remove()
        {
            if (Removed != null)
                Removed(this);

            Destroy(gameObject);
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

    /// <summary>
    /// Binds data to a selectable view.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class UIDataViewSelectable<TData> : UIDataViewSelectable
    {
        public event Action<UIDataViewSelectable<TData>> RemovedData;
        public event Action<UIDataViewSelectable<TData>> HighlightedData;
        public event Action<UIDataViewSelectable<TData>> SelectedData;

        public TData Data { get; private set; }

        protected override void OnHighlight()
        {
            base.OnHighlight();

            if (HighlightedData != null)
                HighlightedData(this);
        }

        protected override void OnSelect()
        {
            base.OnSelect();

            if (SelectedData != null)
                SelectedData(this);
        }

        public void Initialize(TData data)
        {
            OnDefault();
            Data = data;
            OnInitialize(data);
        }

        public virtual void OnInitialize(TData data) { }
    }
}
