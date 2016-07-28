using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    private List<Item> items;

    public int TotalWeight
    {
        get { return items.Sum(x => x.Weight); }
    }

    public Inventory()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
            items.Remove(item);
    }

    public void RemoveItemsInCategory(CategoryType category)
    {
        var itemsInCategory = GetItemsFromCategory(category);
        itemsInCategory.ForEach(x => items.Remove(x));
    }

    public List<Item> GetItemsFromCategory(CategoryType category)
    {
        return items
            .Where(x => x.Category == category)
            .ToList();
    }
}
