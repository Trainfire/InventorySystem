using UnityEngine;
using Framework.UI;

public class UserInterface
{
    private Inventory inventory;

    public UserInterface(Game game)
    {
        inventory = Object.FindObjectOfType<Inventory>();
        inventory.Initialize(game);
    }
}
