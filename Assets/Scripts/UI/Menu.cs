using UnityEngine;
using System.Collections;
using Framework;

public class Menu : MonoBehaviourEx
{
    public UIMenuInventory Inventory;

    protected override void OnShow()
    {
        Inventory.SetVisibility(true);
    }

    protected override void OnHide()
    {
        Inventory.SetVisibility(false);
    }
}
