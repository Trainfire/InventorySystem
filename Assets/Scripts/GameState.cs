using UnityEngine;
using System.Collections;
using Framework;
using System;

/// <summary>
/// Manages the state of the game.
/// </summary>
public class GameState
{
    public State State { get { return stateManager.State; } }

    private StateManager stateManager;

    public GameState(Game game)
    {
        stateManager = new StateManager();

        game.UserInterface.MenuOpened += UserInterface_MenuOpened;
        game.UserInterface.MenuClosed += UserInterface_MenuClosed;
    }

    private void UserInterface_MenuClosed()
    {
        stateManager.SetState(State.Running);
    }

    private void UserInterface_MenuOpened()
    {
        stateManager.SetState(State.Paused);
    }
}
