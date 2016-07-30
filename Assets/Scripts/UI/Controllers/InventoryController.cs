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
        public AnimatorScroller ItemsScroller;
        public UIDataViewList Items;
        public UIItemPreview ItemPreview;

        private ListNavigation navigation;
        private InventoryData inventory;
        private UIDataViewSelectable<ItemData> currentItemView;

        public void Initialize(Game game)
        {
            inventory = game.Data.Inventory;

            ItemsPanel.gameObject.SetActive(false);
            ItemPreview.gameObject.SetActive(false);

            navigation = gameObject.AddComponent<ListNavigation>();
            navigation.Register(Categories);
            navigation.Register(Items);
            navigation.Focused += Navigation_FocusedChanged;

            ItemsScroller = Items.gameObject.AddComponent<AnimatorScroller>();

            InputManager.RegisterHandler(this);

            // Build category list.
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

        private void Navigation_FocusedChanged(UIDataViewList dataViewList)
        {
            ControlList.Clear();

            // Select the first item when the Items list is focused.
            if (dataViewList == Items)
            {
                // Mark the currently highlighted category as Selected.
                Categories.Select();

                ItemsPanel.gameObject.SetActive(true);

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
                Items.ResetAll();
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
            ItemPreview.SetData(dataView.Data);
            ItemsScroller.ScrollTo(dataView.gameObject.transform);
        }

        void IInputHandler.HandleInput(InputActionEvent action)
        {
            // Drop item.
            if (action.Action == InputAction.Drop && action.Type == InputActionType.Down && currentItemView != null)
            {
                inventory.RemoveItem(currentItemView.Data);
                Items.RemoveItem(currentItemView);
            }
        }
    }
}
