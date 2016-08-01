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

        protected override void OnDefault()
        {
            base.OnDefault();
            Name.color = DefaultColor.Color;
        }

        protected override void OnHighlight()
        {
            base.OnHighlight();
            Name.color = HighlightedColor.Color;
        }

        protected override void OnSelect()
        {
            base.OnSelect();
            Name.color = SelectedColor.Color;
        }
    }
}
