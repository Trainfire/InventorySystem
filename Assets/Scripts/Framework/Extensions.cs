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

    static class GameObjectEx
    {
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            var comp = obj.GetComponent<T>();
            if (comp != null)
            {
                return comp;
            }
            else
            {
                return obj.gameObject.AddComponent<T>();
            }
        }

        public static List<Transform> GetChildTransforms(this GameObject obj, bool includeInActive = false)
        {
            var children = obj.GetComponentsInChildren<Transform>(includeInActive);
            var childTransforms = new List<Transform>();
            foreach (var child in children)
            {
                if (child.parent == obj.transform)
                {
                    childTransforms.Add(child);
                }
            }
            return childTransforms;
        }
    }
}
