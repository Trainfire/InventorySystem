using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    static class ListEx
    {
        /// <summary>
        /// Returns true if the index is within range of the list. Always returns false is the list is empty.
        /// </summary>
        public static bool InRange<T>(this IList<T> list, int index)
        {
            if (list.Count == 0)
            {
                return false;
            }
            else
            {
                return index >= 0 && index <= list.Count - 1;
            }
        }
    }
}
