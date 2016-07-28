using UnityEngine;
using System.Collections;
using Models;

namespace UI
{
    public class UIInventoryCategories : MonoBehaviour
    {
        public UIItemList ItemList;

        public void Awake()
        {
            var inventory = new InventoryData();

            inventory.AddItem(new ItemData()
            {
                Name = "Apple",
                Category = CategoryType.Consumable,
            });

            inventory.AddItem(new ItemData()
            {
                Name = "Mighty Sword",
                Category = CategoryType.Weapon,
            });

            inventory.AddItem(new ItemData()
            {
                Name = "Old Hat",
                Category = CategoryType.Misc,
            });

            Initialize(inventory);
        }

        public void Initialize(InventoryData inventory)
        {
            if (ItemList == null)
            {
                Debug.LogError("ItemList is null.");
                return;
            }

            foreach (var item in inventory.Items)
            {
                var view = ItemList.AddItem<UIItemCategory>();
                view.Category = item.Category;
            }
        }
    }
}
