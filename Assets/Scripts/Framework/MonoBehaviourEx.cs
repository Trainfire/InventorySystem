using UnityEngine;
using System.Collections;

namespace Framework
{
    public class MonoBehaviourEx : MonoBehaviour
    {
        private bool alreadyShown;

        /// <summary>
        /// Use this as a substitute for SetActive. Unlike the OnEnable and OnDisable callbacks, the call order for OnShow and OnHide is guaranteed to be in order.
        /// </summary>
        /// <param name="visible"></param>
        public void SetVisibility(bool visible)
        {
            if (visible)
            {
                gameObject.SetActive(true);

                if (!alreadyShown)
                {
                    alreadyShown = true;
                    OnFirstShow();
                }

                OnShow();
            }
            else
            {
                OnHide();
                gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Called once when Show is first invoked. OnShow is called immediately after.
        /// </summary>
        protected virtual void OnFirstShow() { }
        protected virtual void OnShow() { }
        protected virtual void OnHide() { }
    }
}
