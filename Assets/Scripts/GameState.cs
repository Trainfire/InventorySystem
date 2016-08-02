using UnityEngine;
using System.Collections;
using Framework;
using System;

/// <summary>
/// Manages the state of the game.
/// </summary>
public class GameState
{
    public State State { get { return StateManager.State; } }

    public StateManager StateManager;

    public GameState(Game game)
    {
        StateManager = new StateManager();

        game.UserInterface.MenuOpened += UserInterface_MenuOpened;
        game.UserInterface.MenuClosed += UserInterface_MenuClosed;
    }

    private void UserInterface_MenuClosed()
    {
        StateManager.SetState(State.Running);
    }

    private void UserInterface_MenuOpened()
    {
        StateManager.SetState(State.Paused);
    }
}
