using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class InventoryData
    {
        private List<ItemData> items;

        /// <summary>
        /// Returns a copy of the list of items. A COPY!!!
        /// </summary>
        public List<ItemData> Items
        {
            get { return items.ToList(); }
        }

        public int TotalWeight
        {
            get { return items.Sum(x => x.Weight); }
        }

        public InventoryData()
        {
            items = new List<ItemData>();
        }

        public void AddItem(ItemData item)
        {
            items.Add(item);
        }

        public void RemoveItem(ItemData item)
        {
            if (items.Contains(item))
                items.Remove(item);
        }

        public void RemoveItemsInCategory(CategoryType category)
        {
            var itemsInCategory = GetItemsFromCategory(category);
            itemsInCategory.ForEach(x => items.Remove(x));
        }

        public List<ItemData> GetItemsFromCategory(CategoryType category)
        {
            return items
                .Where(x => x.Category == category)
                .ToList();
        }
    }
}