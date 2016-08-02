using UnityEngine;
using System.Collections;

namespace Framework.UI
{
    public class UIDataView<TData> : MonoBehaviour
    {
        public TData Data { get; private set; }

        public void SetData(TData data)
        {
            Data = data;
            OnSetData(data);
        }

        public virtual void OnSetData(TData data) { }

        public void Clear()
        {
            OnClear();
        }

        public virtual void OnClear() { }
    }
}
