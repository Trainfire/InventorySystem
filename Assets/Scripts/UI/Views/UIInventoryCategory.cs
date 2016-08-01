using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.UI
{
    public class UIInventoryCategory : UIDataViewSelectable<CategoryData>
    {
        public Text Name;
        public Text ItemCount;

        public override void OnSetData(CategoryData data)
        {
            base.OnSetData(data);
            Name.enabled = true;
            Name.text = data.CategoryType.ToString();
            ItemCount.enabled = true;
            ItemCount.text = "(" + data.ItemCount + ")";
        }

        public override void OnClear()
        {
            Name.enabled = false;
            ItemCount.enabled = false;
        }

        protected override void OnDefault()
        {
            base.OnDefault();
            Name.color = DefaultColor.Color;
            ItemCount.color = DefaultColor.Color;
        }

        protected override void OnHighlight()
        {
            base.OnHighlight();
            Name.color = HighlightedColor.Color;
            ItemCount.color = HighlightedColor.Color;
        }

        protected override void OnSelect()
        {
            base.OnSelect();
            Name.color = SelectedColor.Color;
            ItemCount.color = SelectedColor.Color;
        }
    }
}
