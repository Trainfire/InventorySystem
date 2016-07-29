using UnityEngine;
using System;
using System.Collections.Generic;
using GameSystems;

namespace UI
{
    /// <summary>
    /// Provides a wrapped list of items that can be navigated using Prev() and Next().
    /// </summary>
    public class UIItemList : MonoBehaviour
    {
        public UIItem Prototype;

        private List<UIItem> items;
        private int index;

        public void Awake()
        {
            items = new List<UIItem>();
            Prototype.gameObject.SetActive(false);
        }

        public T AddItem<T>() where T : UIItem
        {
            if (Prototype == null)
            {
                Debug.LogError("Prototype is missing!");
                return null;
            }

            var instance = UIUtility.Add<T>(transform, Prototype.gameObject);
            instance.Selected += Item_Selected;

            items.Add(instance);

            return instance;
        }

        private void Item_Selected(UIItem item)
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

        public void Select()
        {
            items[index].Select();
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
