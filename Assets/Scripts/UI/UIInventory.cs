using UnityEngine;
using System.Collections;
using Models;

namespace UI
{
    public class UIInventory : MonoBehaviour
    {
        public UIDataViewList Categories;
        public UIDataViewList Items;

        private InventoryData inventory;
        private ItemData currentItem;

        public void Awake()
        {
            inventory = GetMockData();
        }

        public void Start()
        {
            foreach (var category in inventory.GetCategories())
            {
                var view = Categories.AddItem<UIInventoryCategory>();
                view.Initialize(category);
                view.SelectedData += Category_Selected;
            }
        }

        private void Category_Selected(UIDataViewSelectable<CategoryType> dataView)
        {
            Items.Clear();

            foreach (var item in inventory.GetItemsFromCategory(dataView.Data))
            {
                var view = Items.AddItem<UIInventoryItem>();
                view.Initialize(item);
                view.HighlightedData += Item_Highlighted;
                view.RemovedData += Item_Removed;
            }
        }

        private void Item_Removed(UIDataViewSelectable<ItemData> dataView)
        {
            // Cleanup on remove.
            dataView.RemovedData -= Item_Removed;
        }

        private void Item_Highlighted(UIDataViewSelectable<ItemData> dataView)
        {
            // TODO: Update the item preview here.
            currentItem = dataView.Data;
        }

        InventoryData GetMockData()
        {
            var mockData = new InventoryData();

            for (int i = 0; i < 5; i++)
            {
                mockData.AddItem(new ItemData()
                {
                    Name = "Apple",
                    Category = CategoryType.Consumable,
                });

                mockData.AddItem(new ItemData()
                {
                    Name = "Mighty Sword",
                    Category = CategoryType.Weapon,
                });

                mockData.AddItem(new ItemData()
                {
                    Name = "Old Hat",
                    Category = CategoryType.Misc,
                });

                mockData.AddItem(new ItemData()
                {
                    Name = "Dapper Bowtie",
                    Category = CategoryType.Cosmetic,
                });
            }

            return mockData;
        }
    }
}
