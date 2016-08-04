using UnityEngine;
using System;
using System.Collections;

namespace Framework
{
    public class MonoBehaviourEx : MonoBehaviour
    {
        private bool _alreadyShown;

        /// <summary>
        /// If false, the gameobject will still be active after visibility is set to false.
        /// </summary>
        protected bool _disableOnHide = true;

        /// <summary>
        /// Use this as a substitute for SetActive. Unlike the OnEnable and OnDisable callbacks, the call order for OnShow and OnHide is guaranteed to be in order.
        /// </summary>
        /// <param name="visible"></param>
        public void SetVisibility(bool visible)
        {
            if (visible)
            {
                gameObject.SetActive(true);

                if (!_alreadyShown)
                {
                    _alreadyShown = true;
                    OnFirstShow();
                }

                OnShow();
            }
            else
            {
                OnHide();
                if (_disableOnHide)
                    gameObject.SetActive(false);
            }
        }

        public void Kill(Action postKill)
        {
            OnKill();
            postKill();
        }

        protected virtual void OnKill()
        {

        }

        /// <summary>
        /// Called once when Show is first invoked. OnShow is called immediately after.
        /// </summary>
        protected virtual void OnFirstShow() { }
        protected virtual void OnShow() { }
        protected virtual void OnHide() { }
        protected virtual void OnHideOverride() { }
    }
}
