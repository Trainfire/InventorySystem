using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using Framework.Stylesheet;

namespace Framework.UI
{
    public class UIDataViewSelectable : UISelectable, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event Action<UIDataViewSelectable> Removed;        

        public virtual void Awake()
        {
            Default();
        }

        public virtual void OnDestroy()
        {
            if (Removed != null)
                Removed.Invoke(this);
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
