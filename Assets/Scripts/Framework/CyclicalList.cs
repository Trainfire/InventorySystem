using UnityEngine;
using System;
using System.Collections.Generic;

namespace Framework
{
    public enum CycleType
    {
        Forward,
        Backward,
        ToStart,
        ToEnd,
    }

    public class CyclicalListEvent<T> : EventArgs
    {
        public CycleType CycleType { get; private set; }
        public T Data { get; private set; }

        public CyclicalListEvent(CycleType cycleType, T data)
        {
            CycleType = cycleType;
            Data = data;
        }
    }

    /// <summary>
    /// Allows a referenced list to be traversed via MoveNext and MovePrev.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CyclicalList<T>
    {
        public event EventHandler<CyclicalListEvent<T>> Moved;

        /// <summary>
        /// If true, the list will automatically return to first item when moving forward on the last item.
        /// It will automatically return to the last item when moving backwards from the first item.
        /// </summary>
        public bool ShouldWrap { get; set; }

        private List<T> list;
        private int index;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">The reference list to cycle through.</param>
        public CyclicalList(List<T> list)
        {
            this.list = list;
        }

        public void MoveNext()
        {
            if (index < list.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }

            OnMove(CycleType.Forward);
        }

        public void MovePrev()
        {
            if (index > 0)
            {
                index--;
            }
            else
            {
                index = list.Count - 1;
            }

            OnMove(CycleType.Backward);
        }

        public void MoveToStart()
        {
            index = 0;
            OnMove(CycleType.ToStart);
        }

        public void MoveToEnd()
        {
            index = list.Count - 1;
            OnMove(CycleType.ToEnd);
        }

        private void OnMove(CycleType moveType)
        {
            if (Moved != null)
                Moved(this, new CyclicalListEvent<T>(moveType, list[index]));
        }
    }
}
