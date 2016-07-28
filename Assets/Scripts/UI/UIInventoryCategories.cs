using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Models;

namespace UI
{
    public class UIInventoryCategories : MonoBehaviour
    {
        public UIItemList ItemList;
        private List<UIItemCategory> views;

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

            inventory.AddItem(new ItemData()
            {
                Name = "Dapper Bowtie",
                Category = CategoryType.Cosmetic,
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

            views = new List<UIItemCategory>();

            foreach (var item in inventory.Items)
            {
                var view = ItemList.AddItem<UIItemCategory>();
                view.Initialize(item.Category);
                view.Category = item.Category;
                view.SelectedData += View_Selected;
                view.HighlightedData += View_Highlighted;
                views.Add(view);
            }

            // Selects the first item in the list.
            ItemList.JumpToStart();
        }

        private void View_Highlighted(CategoryType obj)
        {
            Debug.Log("You highlighted category: " + obj.ToString());
        }

        private void View_Selected(CategoryType obj)
        {
            Debug.Log("You selected category: " + obj.ToString());
        }

        public void LateUpdate()
        {
            if (Input.GetKeyUp(KeyCode.UpArrow))
                ItemList.Prev();

            if (Input.GetKeyUp(KeyCode.DownArrow))
                ItemList.Next();

            if (Input.GetKeyUp(KeyCode.LeftArrow))
                ItemList.JumpToStart();

            if (Input.GetKeyUp(KeyCode.RightArrow))
                ItemList.JumpToEnd();

            if (Input.GetKeyUp(KeyCode.E))
                ItemList.Select();
        }
    }
}
