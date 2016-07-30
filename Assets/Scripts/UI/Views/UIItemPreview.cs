using UnityEngine;
using UnityEngine.UI;
using Models;

namespace UI
{
    public class UIItemPreview : MonoBehaviour
    {
        public Text Name;

        public void SetData(ItemData item)
        {
            Name.text = item.Name;
        }
    }
}
