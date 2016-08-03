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

    public abstract class CyclicalList
    {
        public abstract void MoveNext();
        public abstract void MovePrev();
        public abstract void MoveToStart();
        public abstract void MoveToEnd();
    }

    /// <summary>
    /// Allows a referenced list to be traversed via MoveNext and MovePrev.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CyclicalList<T> : CyclicalList
    {
        public event EventHandler<CyclicalListEvent<T>> Moved;

        /// <summary>
        /// If true, the list will automatically return to first item when moving forward on the last item,
        /// and will automatically return to the last item when moving backwards from the first item.
        /// </summary>
        public bool Wrapped { get; set; }

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

        /// <summary>
        /// Updates the internal index to match the index of the specified value.
        /// </summary>
        public void Set(T data)
        {
            if (list.Contains(data))
            {
                index = list.IndexOf(data);
            }
        }

        public override void MoveNext()
        {
            if (index < list.Count - 1)
            {
                index++;
                OnMove(CycleType.Forward);
            }
            else if (Wrapped)
            {
                MoveToStart();
            }
        }

        public override void MovePrev()
        {
            if (index > 0)
            {
                index--;
                OnMove(CycleType.Backward);
            }
            else if (Wrapped)
            {
                MoveToEnd();
            }
        }

        public override void MoveToStart()
        {
            index = 0;
            OnMove(CycleType.ToStart);
        }

        public override void MoveToEnd()
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
