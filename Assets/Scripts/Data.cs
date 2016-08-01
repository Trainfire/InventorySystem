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

        for (int i = 0; i < 30; i++)
        {
            mockData.AddItem(new ItemData()
            {
                Name = "Apple " + i,
                Category = CategoryType.Consumable,
            });

            mockData.AddItem(new ItemData()
            {
                Name = "Mighty Sword " + i,
                Category = CategoryType.Weapon,
            });

            mockData.AddItem(new ItemData()
            {
                Name = "Old Hat " + i,
                Category = CategoryType.Misc,
            });

            mockData.AddItem(new ItemData()
            {
                Name = "Dapper Bowtie " + i,
                Category = CategoryType.Cosmetic,
            });
        }

        return mockData;
    }
}
