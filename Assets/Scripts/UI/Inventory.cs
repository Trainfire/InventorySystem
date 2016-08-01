using UnityEngine;
using System.Collections;
using Models;
using Framework;
using System;

namespace Framework.UI
{
    public class Inventory : MonoBehaviour, IInputHandler
    {
        public ControlList ControlList;
        public UIDataViewList Categories;
        public UIDataViewList Items;
        public UIItemPreview ItemPreview;

        private ListNavigation navigation;
        private InventoryData inventory;
        private InputHoldBehaviour holdDropBehaviour;

        private DataViewList<CategoryType, UIInventoryCategory> categoriesDataView;
        private DataViewList<ItemData, UIInventoryItem> itemsDataView;

        private ItemData currentItem;
        private CategoryType selectedCategory;

        public void Initialize(Game game)
        {
            // Get inventory.
            inventory = game.Data.Inventory;

            holdDropBehaviour = new InputHoldBehaviour(InputAction.Drop);
            holdDropBehaviour.OnTrigger += HoldDropBehaviour_OnTrigger;

            // Build category list.
            categoriesDataView = new DataViewList<CategoryType, UIInventoryCategory>(Categories);
            categoriesDataView.Highlighted += CategoriesDataView_Highlighted;
            categoriesDataView.Selected += CategoriesDataView_Selected;
            categoriesDataView.AddRange(inventory.GetCategories());

            // Item data list.
            itemsDataView = new DataViewList<ItemData, UIInventoryItem>(Items);
            itemsDataView.Highlighted += ItemsDataView_Highlighted;

            // Handles navigation between the Category and Items panel.
            navigation = gameObject.GetComponent<ListNavigation>();
            navigation.Register(categoriesDataView);
            navigation.Register(itemsDataView);
            navigation.Focused += Navigation_FocusedChanged;

            // Register for input.
            InputManager.RegisterHandler(this);

            // Focus on Categories by default and show items from the first category.
            navigation.Focus(categoriesDataView);
            categoriesDataView.Select();
            categoriesDataView.Highlight();
        }

        private void CategoriesDataView_Selected(CategoryType category)
        {
            selectedCategory = category;
        }

        private void CategoriesDataView_Highlighted(CategoryType category)
        {
            // Clear all items once a category is selected before repopulating it with the new items.
            itemsDataView.Clear();
            itemsDataView.AddRange(inventory.GetItemsFromCategory(category));

            // Move to beginning of list if a different category from the previously selected.
            if (category != selectedCategory)
                itemsDataView.MoveToStart();

            // Highlight first item.
            itemsDataView.Highlight();
        }

        private void Navigation_FocusedChanged(UIDataViewList dataViewList)
        {
            // Clear the buttons displayed at the bottom of the UI.
            ControlList.Clear();

            // Select the first item when the Items list is focused.
            if (dataViewList == Items)
            {
                // Mark the currently highlighted category as Selected.
                categoriesDataView.Select();

                // If the same category is selected, leave the highlighted view alone.
                if (categoriesDataView.Current != selectedCategory)
                {
                    // Otherwise highlight the first item.
                    itemsDataView.ResetAll();
                    itemsDataView.MoveToStart();
                    itemsDataView.Highlight();
                }

                // Display controls.
                ControlList.AddControl(new ControlData(InputAction.Equip, "Equip"));
                ControlList.AddControl(new ControlData(InputAction.Drop, "Drop"));
            }

            if (dataViewList == Categories)
                categoriesDataView.Highlight();
        }

        private void ItemsDataView_Highlighted(ItemData item)
        {
            currentItem = item;

            // Update preview.
            ItemPreview.SetData(item);
        }

        private void HoldDropBehaviour_OnTrigger()
        {
            DropItem();
        }

        void DropItem()
        {
            if (currentItem != null)
            {
                inventory.RemoveItem(currentItem);
                itemsDataView.Remove(currentItem);

                if (itemsDataView.Count == 0)
                    ItemPreview.gameObject.SetActive(false);
            }
        }

        void IInputHandler.HandleInput(InputActionEvent action)
        {
            if (action.Action == InputAction.Drop && action.Type == InputActionType.Down)
                DropItem();
        }
    }
}
