using Models;

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

        for (int i = 0; i < 5; i++)
        {
            mockData.AddItem(new ItemData()
            {
                Name = "Apple",
                Category = CategoryType.Consumable,
            });

            mockData.AddItem(new ItemData()
            {
                Name = "Mighty Sword",
                Category = CategoryType.Weapon,
            });

            mockData.AddItem(new ItemData()
            {
                Name = "Old Hat",
                Category = CategoryType.Misc,
            });

            mockData.AddItem(new ItemData()
            {
                Name = "Dapper Bowtie",
                Category = CategoryType.Cosmetic,
            });
        }

        return mockData;
    }
}
