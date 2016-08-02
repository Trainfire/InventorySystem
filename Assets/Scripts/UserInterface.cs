using UnityEngine;
using Framework;
using Framework.UI;
using System;

public class UserInterface : MonoBehaviour, IGameDependent, IInputHandler
{
    public event Action MenuOpened;
    public event Action MenuClosed;

    public Menu menu;
    public UIHud Hud;
    public Inventory Inventory;

    private bool menuOpen;

    public void Initialize(Game game)
    {
        Inventory.Initialize(game.Data.Inventory);

        InputManager.RegisterHandler(this);
    }

    void IInputHandler.HandleInput(InputActionEvent action)
    {
        // Toggle menu open state.
        if (action.Action == InputAction.Inventory && action.Type == InputActionType.Down)
        {
            if (menuOpen)
            {
                menuOpen = false;
                if (MenuClosed != null)
                    MenuClosed();
            }
            else
            {
                menuOpen = true;
                if (MenuOpened != null)
                    MenuOpened();
            }

            menu.gameObject.SetActive(menuOpen);
            Hud.gameObject.SetActive(!menuOpen);
        }
    }
}
