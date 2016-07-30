using UnityEngine;
using System.Collections;
using Models;

namespace UI
{
    public class InventoryController : MonoBehaviour
    {
        public UIDataViewList Categories;
        public UIDataViewList Items;

        private ListNavigation navigation;
        private InventoryData inventory;
        private ItemData currentItem;

        public void Initialize(Game game)
        {
            navigation = new ListNavigation(game.InputManager);
            navigation.Register(Categories);
            navigation.Register(Items);

            navigation.Focused += Navigation_OnFocus;

            inventory = game.Data.Inventory;

            foreach (var category in inventory.GetCategories())
            {
                var view = Categories.AddItem<UIInventoryCategory>();
                view.Initialize(category);
                view.HighlightedData += Category_Highlighted;
            }
        }

        private void Navigation_OnFocus(UIDataViewList dataViewList)
        {
            // Select the first item when the Items list is focused.
            if (dataViewList == Items)
                Items.Highlight(0);
        }

        /// <summary>
        /// Temporary until we can tell whether where using mouse input or control-based input.
        /// </summary>
        /// <param name="dataView"></param>
        private void Category_Highlighted(UIDataViewSelectable<CategoryType> dataView)
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
    }
}
