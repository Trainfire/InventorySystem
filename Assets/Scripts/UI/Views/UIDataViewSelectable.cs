using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Stylesheet;

namespace UI
{
    public class UIDataViewSelectable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event Action<UIDataViewSelectable> Removed;
        public event Action<UIDataViewSelectable> Defaulted;
        public event Action<UIDataViewSelectable> Highlighted;
        public event Action<UIDataViewSelectable> Selected;

        public ColorData SelectedColor;
        public ColorData HighlightedColor;
        public ColorData DefaultColor;

        public bool SelectableByMouse;

        public bool IsSelected { get; private set; }

        public virtual void OnAwake()
        {
            OnDefault();
        }

        public virtual void OnDestroy()
        {
            if (Removed != null)
                Removed(this);
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
            if (!IsSelected && SelectableByMouse)
                Default();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (!IsSelected && SelectableByMouse)
                Highlight();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (SelectableByMouse)
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

        public void SetData(TData data)
        {
            OnDefault();
            Data = data;
            OnSetData(data);
        }

        public virtual void OnSetData(TData data) { }

        public void Clear()
        {
            OnClear();
        }

        public virtual void OnClear() { }

        public override void OnDestroy()
        {
            base.OnDestroy();

            if (RemovedData != null)
                RemovedData(this);
        }
    }
}
