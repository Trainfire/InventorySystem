using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIItemCategory : UIItem<CategoryType>
    {
        public Text Name;

        private CategoryType category;
        
        public CategoryType Category
        {
            set
            {
                category = value;
                Name.text = category.ToString().ToUpper();
            }
        }

        protected override void OnDefault()
        {
            base.OnDefault();
            Debug.Log("Defaulting...");
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
