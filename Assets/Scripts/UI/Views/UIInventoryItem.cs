using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Framework.UI
{
    public class UIInventoryItem : UIDataViewSelectable<ItemData>
    {
        public Text Name;

        public override void OnSetData(ItemData data)
        {
            Name.enabled = true;
            Name.text = data.Name;
        }

        public override void OnClear()
        {
            Name.enabled = false;
        }
    }
}
