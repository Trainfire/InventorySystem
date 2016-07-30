using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UI
{
    /// <summary>
    /// Provides a wrapped list of items that can be navigated using Prev() and Next().
    /// </summary>
    public class UIDataViewList : MonoBehaviour
    {
        public event UnityAction<UIDataViewSelectable> MovePrevious;
        public event UnityAction<UIDataViewSelectable> MoveNext;
        public event UnityAction<UIDataViewSelectable> Highlighted;

        public UIDataViewSelectable Prototype;

        public int Count { get { return items.Count; } }

        private List<UIDataViewSelectable> items;
        private int index;

        public void Awake()
        {
            items = new List<UIDataViewSelectable>();
            Prototype.gameObject.SetActive(false);
        }

        public T AddItem<T>() where T : UIDataViewSelectable
        {
            if (Prototype == null)
            {
                Debug.LogError("Prototype is missing!");
                return null;
            }

            var instance = UIUtility.Add<T>(transform, Prototype.gameObject);
            instance.Selected += Item_Selected;
            instance.Removed += Item_Removed;

            items.Add(instance);

            return instance;
        }

        public void RemoveItem(UIDataViewSelectable item)
        {
            if (item == null)
                return;

            if (!items.Contains(item))
            {
                Debug.LogWarningFormat("Item '{0}' does not exist in this list.", item.name);
                return;
            }

            bool isEnd = items.IndexOf(item) == items.Count - 1;

            Destroy(item.gameObject);
            items.Remove(item);

            if (items.Count != 0)
            {
                // If we remove the last item, jump to the end of the resized list.
                if (isEnd)
                {
                    JumpToEnd();
                }
                else
                {
                    Highlight();
                }
            }
        }

        public void Clear()
        {
            items.ForEach(x => Destroy(x.gameObject));
            items.Clear();
        }

        private void Item_Removed(UIDataViewSelectable item)
        {
            item.Removed -= Item_Removed;
            item.Selected -= Item_Selected;
        }

        private void Item_Selected(UIDataViewSelectable item)
        {
            ResetAll();
            index = items.IndexOf(item);
        }

        public void Prev()
        {
            ResetAll();
            Cycle(-1);
            Highlight();

            if (MovePrevious != null && items.Count != 0)
                MovePrevious(items[index]);
        }

        public void Next()
        {
            ResetAll();
            Cycle(1);
            Highlight();

            if (MoveNext != null && items.Count != 0)
                MoveNext(items[index]);
        }

        public void JumpToStart()
        {
            ResetAll();
            index = 0;
            Highlight();
        }

        public void JumpToEnd()
        {
            ResetAll();
            index = items.Count - 1;
            Highlight();
        }

        public void Highlight()
        {
            Highlight(index);
        }

        public void Highlight(int index)
        {
            if (items.Count == 0)
                return;

            if (!items.InRange(index))
            {
                Debug.LogError("Index is out of range.");
            }
            else
            {
                this.index = index;
                items[index].Highlight();

                if (Highlighted != null)
                    Highlighted(items[index]);
            }
        }

        public void Select()
        {
            Select(index);
        }

        public void Select(int index)
        {
            if (!items.InRange(index))
            {
                Debug.LogError("Index is out of range.");
            }
            else
            {
                this.index = index;
                items[index].Select();
            }
        }

        public void ResetAll()
        {
            items.ForEach(x => x.Default());
        }

        void Cycle(int direction)
        {
            if (direction > 0)
            {
                if (index == items.Count - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
            else
            {
                if (index == 0)
                {
                    index = items.Count - 1;
                }
                else
                {
                    index--;
                }
            }
        }
    }
}
