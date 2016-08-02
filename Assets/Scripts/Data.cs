using Models;
using UnityEngine;
using System;

public class Data
{
    public InventoryData Inventory { get; private set; }

    public Data()
    {
        Inventory = GetMockData();
    }

    InventoryData GetMockData()
    {
        var mockData = new InventoryData();

        for (int i = 0; i < 30; i++)
        {
            mockData.AddItem(MakeItem(item =>
            {
                item.Name = "Apple " + i;
                item.Category = CategoryType.Consumable;
            }));

            mockData.AddItem(MakeItem(item =>
            {
                item.Name = "Mighty Sword " + i;
                item.Category = CategoryType.Weapon;
            }));

            mockData.AddItem(MakeItem(item =>
            {
                item.Name = "Old Hat " + i;
                item.Category = CategoryType.Cosmetic;
            }));

            mockData.AddItem(MakeItem(item =>
            {
                item.Name = "Dapper Bowtier " + i;
                item.Category = CategoryType.Cosmetic;
            }));
        }

        return mockData;
    }

    ItemData MakeItem(Action<ItemData> onAdd)
    {
        var instance = ScriptableObject.CreateInstance<ItemData>();
        onAdd(instance);
        return instance;
    }
}
