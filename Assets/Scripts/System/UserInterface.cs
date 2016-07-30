using UnityEngine;
using UI;

public class UserInterface
{
    private InventoryController inventory;

    public UserInterface(Game game)
    {
        inventory = Object.FindObjectOfType<InventoryController>();
        inventory.Initialize(game);
    }
}
