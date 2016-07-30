using UnityEngine;
using System.Collections;
using Models;

namespace UI
{
    public class InventoryController : MonoBehaviour
    {
        public UIDataViewList Categories;
        public UIDataViewList Items;
        public UIItemPreview ItemPreview;

        private ListNavigation navigation;
        private InventoryData inventory;
        private ItemData currentItem;

        public void Initialize(Game game)
        {
            inventory = game.Data.Inventory;

            ItemPreview.gameObject.SetActive(false);

            navigation = new ListNavigation(game.InputManager);
            navigation.Register(Categories);
            navigation.Register(Items);
            navigation.Focused += Navigation_OnFocus;

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

        private void Navigation_OnFocus(UIDataViewList dataViewList)
        {
            // Select the first item when the Items list is focused.
            if (dataViewList == Items)
            {
                Categories.Select();
                Items.Highlight(0);
                ItemPreview.gameObject.SetActive(true);
            }

            if (dataViewList == Categories)
            {
                Categories.Highlight();
                Items.ResetAll();
                ItemPreview.gameObject.SetActive(false);
            }
        }

        private void Item_Removed(UIDataViewSelectable<ItemData> dataView)
        {
            // Cleanup on remove.
            dataView.RemovedData -= Item_Removed;
        }

        private void Item_Highlighted(UIDataViewSelectable<ItemData> dataView)
        {
            currentItem = dataView.Data;
            ItemPreview.SetData(currentItem);
        }
    }
}
