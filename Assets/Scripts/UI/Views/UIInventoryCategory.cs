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
            Name.text = data.ToString().ToUpper();
        }

        protected override void OnDefault()
        {
            base.OnDefault();
            Name.color = new Color(0.4f, 0.4f, 0.4f);
        }

        protected override void OnHighlight()
        {
            base.OnHighlight();
            Name.color = Color.white;
        }

        protected override void OnSelect()
        {
            base.OnSelect();
            Name.color = new Color(0.7f, 0.7f, 0.7f);
        }
    }
}
