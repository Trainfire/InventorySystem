using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.UI;

namespace Framework.UI
{
    public abstract class DataViewList
    {
        private UIDataViewList dataView;

        public UIDataViewList DataView
        {
            get { return dataView; }
            protected set { dataView = value; }
        }

        public abstract void MovePrev();
        public abstract void MoveNext();
        public abstract void MoveToStart();
        public abstract void MoveToEnd();
    }

    public class DataViewList<TData, TView> : DataViewList where TView : UIDataViewSelectable<TData>
    {
        public event Action<TData> Highlighted;
        public event Action<TData> Selected;
        public event Action<TData> Removed;

        private int dataIndex;
        private int viewIndex;
        private List<UIDataViewSelectable<TData>> views;
        private List<TData> data;

        public int Count
        {
            get { return data.Count; }
        }

        public TData CurrentData
        {
            get { return data.Count != 0 ? data[viewIndex] : default(TData); }
        }

        public UIDataViewSelectable<TData> CurrentView
        {
            get { return views[viewIndex]; }
        }

        public DataViewList(UIDataViewList dataView)
        {
            DataView = dataView;
            DataView.Controller = this;

            views = new List<UIDataViewSelectable<TData>>();
            data = new List<TData>();

            BuildViews();
        }

        public void Add(TData item)
        {
            if (!data.Contains(item))
            {
                // Error here.
            }
            else
            {
                data.Add(item);
                UpdateViews();
            }
        }

        public void AddRange(List<TData> items)
        {
            data.AddRange(items);
            UpdateViews();
        }

        public void Remove(TData item)
        {
            if (!data.Contains(item))
            {
                // Error here.
            }
            else
            {
                int indexOf = data.IndexOf(item);
                data.Remove(item);
                UpdateViews();

                // Move to next item, otherwise move back.
                if (data.Count != 0)
                {
                    // If we remove the first item in the current view, go back one item.
                    if (dataIndex != 0 && viewIndex == 0)
                    {
                        MovePrev();
                    }

                    // If we remove the last item, jump to the end of the resized list.
                    if (indexOf == data.Count || viewIndex == DataView.MaxViews)
                    {
                        viewIndex = LastViewIndex();
                    }

                    Highlight();
                }

                if (Removed != null)
                    Removed(item);
            }
        }

        /// <summary>
        /// Returns views to their default state.
        /// </summary>
        public void ResetAll()
        {
            views.ForEach(x => x.Default());
        }

        void BuildViews()
        {
            for (int i = 0; i < DataView.MaxViews; i++)
            {
                var view = UIUtility.Add<TView>(DataView.transform, DataView.Prototype.gameObject);

                view.SelectedData += View_SelectedData;
                view.HighlightedData += View_HighlightedData;

                views.Add(view);
            }
        }

        void UpdateViews()
        {
            // Build a list of data that is the size of the maximum amount of views.
            var pageData = data
                .Skip(dataIndex)
                .Take(DataView.MaxViews)
                .ToList();

            for (int i = 0; i < DataView.MaxViews; i++)
            {
                if (pageData.InRange(i))
                {
                    // Update view.
                    views[i].SetData(pageData[i]);
                }
                else
                {
                    // Clear the view as there no data for it at the specified index.
                    views[i].Clear();
                }
            }

            DataView.EnableDownArrow = !IsDataEnd();
            DataView.EnableUpArrow = dataIndex != 0;
        }

        void View_SelectedData(UIDataViewSelectable<TData> view)
        {
            if (Selected != null)
                Selected(view.Data);
        }

        void View_HighlightedData(UIDataViewSelectable<TData> view)
        {
            if (Highlighted != null)
                Highlighted(view.Data);
        }

        public void Clear()
        {
            data.Clear();
            UpdateViews();
        }

        public override void MoveNext()
        {
            if (IsViewEnd() && IsDataEnd())
            {
                MoveToStart();
            }
            else
            {
                if (IsViewEnd())
                    dataIndex++;

                if (!IsViewEnd())
                    viewIndex++;
            }

            UpdateViews();

            Highlight();
        }

        public override void MovePrev()
        {
            if (viewIndex == 0 && dataIndex == 0)
            {
                MoveToEnd();
            }
            else
            {
                if (viewIndex == 0)
                    dataIndex--;

                if (viewIndex != 0)
                    viewIndex--;

                DataView.EnableUpArrow = dataIndex == 0;
            }

            UpdateViews();

            Highlight();
        }

        public override void MoveToStart()
        {
            dataIndex = 0;
            viewIndex = 0;
            UpdateViews();
        }

        public override void MoveToEnd()
        {
            // Move to the last page of items if there's more than one page.
            // Otherwise, just set the index to 0.
            dataIndex = data.Count > DataView.MaxViews ? data.Count - DataView.MaxViews : 0;
            viewIndex = LastViewIndex();
            UpdateViews();
        }

        public void Highlight()
        {
            Highlight(viewIndex);
        }

        public void Highlight(int index)
        {
            if (views.Count == 0)
                return;

            if (!views.InRange(index))
            {
                Debug.LogError("Index is out of range.");
            }
            else
            {
                ResetAll();
                viewIndex = index;
                views[viewIndex].Highlight();
            }
        }

        public void Select()
        {
            Select(viewIndex);
        }

        public void Select(int index)
        {
            if (!views.InRange(index))
            {
                Debug.LogError("Index is out of range.");
            }
            else
            {
                viewIndex = index;
                views[viewIndex].Select();
            }
        }

        /// <summary>
        /// Returns true if the last data population is currently being shown.
        /// </summary>
        /// <returns></returns>
        bool IsDataEnd()
        {
            return dataIndex + DataView.MaxViews >= data.Count;
        }

        /// <summary>
        /// Returns true if the view index is at the end of list of data.
        /// </summary>
        /// <returns></returns>
        bool IsViewEnd()
        {
            if (data.Count < DataView.MaxViews)
                return viewIndex == data.Count - 1;
            return viewIndex == DataView.MaxViews - 1;
        }

        /// <summary>
        /// Returns the index of the last view that contains data. (IE, is visible.)
        /// </summary>
        /// <returns></returns>
        int LastViewIndex()
        {
            return Math.Max(0, data.Skip(dataIndex).Take(DataView.MaxViews).ToList().Count - 1);
        }
    }
}
