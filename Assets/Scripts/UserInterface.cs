using UnityEngine;
using Framework.UI;

public class UserInterface : MonoBehaviour, IGameDependent
{
    public Inventory Inventory;

    public void Initialize(Game game)
    {
        Inventory.Initialize(game.Data.Inventory);
    }
}
