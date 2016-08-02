using UnityEngine;

namespace Framework.UI
{
    class UIUtility
    {
        public static T Add<T>(Transform parent, GameObject prototype)
        {
            var instance = GameObject.Instantiate(prototype.gameObject);
            instance.transform.SetParent(parent, false);
            instance.transform.localScale = Vector3.one;
            instance.SetActive(true);
            return instance.GetComponent<T>();
        }
    }
}
