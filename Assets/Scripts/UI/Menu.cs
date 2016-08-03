using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Framework;

public class Menu : MonoBehaviourEx
{
    [SerializeField] private MenuInventory _inventory;
    [SerializeField] private List<MenuBase> _menus;
    [SerializeField] private MenuButtons _menuButtons;

    public CyclicalList<MenuBase> CyclicalList { get; private set; }
    public MenuBase CurrentMenu { get; private set; }

    private MenuAnimation _animation;
    private MenuInput _menuInput;

    protected override void OnFirstShow()
    {
        _disableOnHide = false;
        _animation = gameObject.AddComponent<MenuAnimation>();
        _animation.TransitionOutFinished += Animation_TransitionOutFinished;

        CyclicalList = new CyclicalList<MenuBase>(_menus);
        CyclicalList.Wrapped = true;
        CyclicalList.Moved += Menu_Moved;

        // Menu input.
        _menuInput = new MenuInput(this);
        _menuInput.InputEnabled = true;

        // Make sure all menus are hidden by default and set the current menu.
        for (int i = 0; i < _menus.Count; i++)
        {
            // Add a button for each menu.
            _menuButtons.AddButton(_menus[i], _menus[i].name);

            if (i == 0)
                CurrentMenu = _menus[i];

            _menus[i].gameObject.SetActive(false);
        }

        _menuButtons.ButtonPressed += MenuButton_OnPressed;
    }

    protected override void OnShow()
    {
        _menuInput.InputEnabled = true;
        CurrentMenu.gameObject.GetComponent<InputGroupHandler>((comp) => comp.InputEnabled = true);
        CurrentMenu.SetVisibility(true);
        _animation.TransitionIn();
    }

    protected override void OnHide()
    {
        _menuInput.InputEnabled = false;
        _animation.TransitionOut();
        CurrentMenu.gameObject.GetComponent<InputGroupHandler>((comp) => comp.InputEnabled = false);
    }

    public void SetMenu(MenuBase menu)
    {
        if (_menus.Contains(menu) && CurrentMenu != null && menu != CurrentMenu)
        {
            CurrentMenu.SetVisibility(false);
            CurrentMenu = menu;
            CurrentMenu.SetVisibility(true);
        }
    }

    private void MenuButton_OnPressed(MenuBase menu)
    {
        SetMenu(menu);
    }

    private void Menu_Moved(object sender, CyclicalListEvent<MenuBase> cycleEvent)
    {
        SetMenu(cycleEvent.Data);
    }

    private void Animation_TransitionOutFinished()
    {
        _inventory.SetVisibility(false);
        _inventory.gameObject.SetActive(false);
    }
}

/// <summary>
/// Handles menu input and passes down recieved input into the currently displayed menu.
/// </summary>
public class MenuInput : IInputHandler
{
    private Menu _menu;

    public bool InputEnabled { get; set; }

    public MenuInput(Menu menu)
    {
        _menu = menu;

        InputManager.RegisterHandler(this);
    }

    void IInputHandler.HandleInput(InputActionEvent action)
    {
        if (!InputEnabled)
            return;

        _menu.CurrentMenu.HandleInput(action);

        if (action.Type == InputActionType.Down)
        {
            if (action.Action == InputAction.LeftBumper)
                _menu.CyclicalList.MovePrev();

            if (action.Action == InputAction.RightBumper)
                _menu.CyclicalList.MoveNext();
        }
    }
}
