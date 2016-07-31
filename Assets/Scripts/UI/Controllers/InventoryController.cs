using UnityEngine;
using System.Collections;
using Models;
using InputSystem;
using System;

namespace UI
{
    public class InventoryController : MonoBehaviour, IInputHandler
    {
        public ControlList ControlList;
        public UIDataViewList Categories;
        public GameObject ItemsPanel;
        public UIDataViewList Items;
        public UIItemPreview ItemPreview;

        private ListNavigation navigation;
        private InventoryData inventory;
        private UIDataViewSelectable<ItemData> currentItemView;
        private InputHoldBehaviour holdDropBehaviour;

        public void Initialize(Game game)
        {
            // Get inventory.
            inventory = game.Data.Inventory;

            holdDropBehaviour = new InputHoldBehaviour(InputAction.Drop);
            holdDropBehaviour.OnTrigger += HoldDropBehaviour_OnTrigger;

            ItemsPanel.gameObject.SetActive(false);
            ItemPreview.gameObject.SetActive(false);

            // Handles navigation between the Category and Items panel.
            navigation = gameObject.AddComponent<ListNavigation>();
            navigation.Register(Categories);
            navigation.Register(Items);
            navigation.Focused += Navigation_FocusedChanged;

            // Register for input.
            InputManager.RegisterHandler(this);

            // Build category list.
            foreach (var category in inventory.GetCategories())
            {
                var view = Categories.AddItem<UIInventoryCategory>();
                view.Initialize(category);
                view.SelectedData += Category_Selected;
            }

            Categories.Highlight();
        }

        private void Category_Selected(UIDataViewSelectable<CategoryType> dataView)
        {
            // Clear all items once a category is selected before repopulating it with the new items.
            Items.Clear();

            foreach (var item in inventory.GetItemsFromCategory(dataView.Data))
            {
                var view = Items.AddItem<UIInventoryItem>();
                view.Initialize(item);
                view.HighlightedData += Item_Highlighted;
                view.RemovedData += Item_Removed;
            }
        }

        private void Navigation_FocusedChanged(UIDataViewList dataViewList)
        {
            // Clear the buttons displayed at the bottom of the UI.
            ControlList.Clear();

            // Select the first item when the Items list is focused.
            if (dataViewList == Items)
            {
                // Mark the currently highlighted category as Selected.
                Categories.Select();

                ItemsPanel.gameObject.SetActive(true);

                // Highlight first item by default.
                if (Items.Count != 0)
                {
                    Items.Highlight(0);
                    ItemPreview.gameObject.SetActive(true);
                }

                // Display controls.
                ControlList.AddControl(new ControlData(InputAction.Equip, "Equip"));
                ControlList.AddControl(new ControlData(InputAction.Drop, "Drop"));
            }

            if (dataViewList == Categories)
            {
                Categories.Highlight();

                ItemsPanel.gameObject.SetActive(false);
                ItemPreview.gameObject.SetActive(false);

                currentItemView = null;
            }
        }

        private void Item_Removed(UIDataViewSelectable<ItemData> dataView)
        {
            dataView.RemovedData -= Item_Removed;

            if (dataView == currentItemView)
                currentItemView = null;

            if (Items.Count == 0)
                ItemPreview.gameObject.SetActive(false);
        }

        private void Item_Highlighted(UIDataViewSelectable<ItemData> dataView)
        {
            currentItemView = dataView;

            // Update preview.
            ItemPreview.SetData(dataView.Data);
        }

        private void HoldDropBehaviour_OnTrigger()
        {
            DropItem();
        }

        void DropItem()
        {
            if (currentItemView != null)
            {
                inventory.RemoveItem(currentItemView.Data);
                Items.RemoveItem(currentItemView);
            }
        }

        void IInputHandler.HandleInput(InputActionEvent action)
        {
            if (action.Action == InputAction.Drop && action.Type == InputActionType.Down)
                DropItem();
        }
    }
}
