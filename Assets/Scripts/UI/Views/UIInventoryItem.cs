using UnityEngine;
using UnityEngine.UI;
using Models;

namespace UI
{
    public class UIInventoryItem : UIDataViewSelectable<ItemData>
    {
        public Text Name;

        public override void OnInitialize(ItemData data)
        {
            Name.text = data.Name;
        }

        protected override void OnDefault()
        {
            base.OnDefault();
            Name.color = Color.grey;
        }

        protected override void OnHighlight()
        {
            base.OnHighlight();
            Name.color = Color.white;
        }

        protected override void OnSelect()
        {
            base.OnSelect();
            Name.color = Color.blue;
        }
    }
}
