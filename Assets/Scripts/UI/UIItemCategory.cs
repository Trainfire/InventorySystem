using UnityEngine;
using UnityEngine.UI;
using System;

namespace UI
{
    public class UIItemCategory : UIItem
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
    }
}
