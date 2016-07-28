using UnityEngine;
using System;
using System.Collections.Generic;
using GameSystems;

namespace UI
{
    public class UIItemList : MonoBehaviour
    {
        public UIItem Prototype;

        private List<UIItem> items;

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

            return UIUtility.Add<T>(transform, Prototype.gameObject);
        }
    }
}
