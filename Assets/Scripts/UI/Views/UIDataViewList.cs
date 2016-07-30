using UnityEngine;
using System;
using System.Collections.Generic;
using GameSystems;

namespace UI
{
    /// <summary>
    /// Provides a wrapped list of items that can be navigated using Prev() and Next().
    /// </summary>
    public class UIDataViewList : MonoBehaviour
    {
        public UIDataViewSelectable Prototype;

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

        public void Clear()
        {
            items.ForEach(x => x.Remove());
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
            items[index].Highlight();
        }

        public void Next()
        {
            ResetAll();
            Cycle(1);
            items[index].Highlight();
        }

        public void JumpToStart()
        {
            ResetAll();
            index = 0;
            items[index].Highlight();
        }

        public void JumpToEnd()
        {
            ResetAll();
            index = items.Count - 1;
            items[index].Highlight();
        }

        public void Highlight()
        {
            Highlight(index);
        }

        public void Highlight(int index)
        {
            if (index < 0 || index > items.Count - 1)
            {
                Debug.LogError("Index is out of range.");
            }
            else
            {
                this.index = index;
                items[index].Highlight();
            }
        }

        public void Select()
        {
            Select(index);
        }

        public void Select(int index)
        {
            if (index < 0 || index > items.Count - 1)
            {
                Debug.LogError("Index is out of range.");
            }
            else
            {
                this.index = index;
                items[index].Select();
            }
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

        void ResetAll()
        {
            items.ForEach(x => x.Default());
        }
    }
}
