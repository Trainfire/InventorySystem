using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIInventoryCategory : UIDataViewSelectable<CategoryType>
    {
        public Text Name;

        public override void OnInitialize(CategoryType data)
        {
            base.OnInitialize(data);
            Name.text = data.ToString();
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
