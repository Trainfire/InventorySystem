using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Framework.UI
{
    public class UIDataViewList : MonoBehaviour
    {
        public GameObject UpArrow;
        public GameObject DownArrow;
        public UIDataViewSelectable Prototype;
        public int MaxViews;

        public DataViewList Controller { get; set; }

        public bool EnableUpArrow
        {
            set
            {
                if (UpArrow != null)
                    UpArrow.SetActive(value);
            }
        }

        public bool EnableDownArrow
        {
            set
            {
                if (DownArrow != null)
                    DownArrow.SetActive(value);
            }
        }

        public void Awake()
        {
            Prototype.gameObject.SetActive(false);
        }
    }
}
