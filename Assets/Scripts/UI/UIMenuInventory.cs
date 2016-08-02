using UnityEngine;
using System.Collections;
using Models;
using Framework;
using Framework.UI;
using System;

public class UIMenuInventory : MonoBehaviourEx, IInputHandler
{
    public ControlList ControlList;
    public UIDataViewList Categories;
    public UIDataViewList Items;
    public UIItemPreview ItemPreview;

    private InputGroupHandler _inputGroupHandler;
    private ListNavigation navigation;
    private InventoryData inventory;
    private InputHoldBehaviour holdDropBehaviour;

    private DataViewList<CategoryData, UIInventoryCategory> categoriesDataView;
    private DataViewList<ItemData, UIInventoryItem> itemsDataView;

    private ItemData currentItem;
    private CategoryType selectedCategory;

    public void Initialize(InventoryData inventory)
    {
        this.inventory = inventory;
    }

    protected override void OnFirstShow()
    {
        base.OnFirstShow();

        holdDropBehaviour = new InputHoldBehaviour(InputAction.Drop);
        holdDropBehaviour.OnTrigger += HoldDropBehaviour_OnTrigger;

        // Build category list.
        categoriesDataView = new DataViewList<CategoryData, UIInventoryCategory>(Categories);
        categoriesDataView.Highlighted += CategoriesDataView_Highlighted;
        categoriesDataView.Selected += CategoriesDataView_Selected;

        // Item data list.
        itemsDataView = new DataViewList<ItemData, UIInventoryItem>(Items);
        itemsDataView.Highlighted += ItemsDataView_Highlighted;
        itemsDataView.Removed += ItemsDataView_Removed;

        // Handles navigation between the Category and Items panel.
        navigation = gameObject.GetComponent<ListNavigation>();
        navigation.Register(categoriesDataView);
        navigation.Register(itemsDataView);
        navigation.Focused += Navigation_FocusedChanged;

        _inputGroupHandler = gameObject.AddComponent<InputGroupHandler>();
        _inputGroupHandler.InputEnabled = true;
        _inputGroupHandler.RegisterInputHandler(this);
        _inputGroupHandler.RegisterInputHandler(navigation);
    }

    protected override void OnShow()
    {
        base.OnShow();

        // Update categories.
        categoriesDataView.AddRange(inventory.GetCategories());

        // Focus on Categories by default and show items from the first category.
        navigation.Focus(categoriesDataView);
        categoriesDataView.Select();
        categoriesDataView.Highlight();
    }

    protected override void OnHide()
    {
        base.OnHide();

        categoriesDataView.Clear();
        itemsDataView.Clear();
    }

    private void CategoriesDataView_Selected(CategoryData category)
    {
        selectedCategory = category.CategoryType;
    }

    private void CategoriesDataView_Highlighted(CategoryData category)
    {
        // Clear all items once a category is selected before repopulating it with the new items.
        itemsDataView.Clear();
        itemsDataView.AddRange(inventory.GetItemsFromCategory(category.CategoryType));

        // Move to beginning of list if a different category from the previously selected.
        if (category.CategoryType != selectedCategory)
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
            if (categoriesDataView.CurrentData.CategoryType != selectedCategory)
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

    private void ItemsDataView_Removed(ItemData item)
    {
        // Update the category data when an item is removed from the category.
        categoriesDataView.Clear();
        categoriesDataView.AddRange(inventory.GetCategories());
        categoriesDataView.Select();
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
