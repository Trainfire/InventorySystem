using UnityEngine;
using Framework;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private UserInterface UserInterface;
    [SerializeField] private PlayerInput PlayerInput;
    [SerializeField] private MonoEventRelay MonoEventRelay;

    public void Awake()
    {
        // Create the Game wrapper.
        var game = new Game(MonoEventRelay, UserInterface);

        // Inject dependency here.
        UserInterface.Initialize(game);
        PlayerInput.Initialize(game);
    }
}

public class Game
{
    public MonoEventRelay MonoEventRelay { get; private set; }
    public UserInterface UserInterface { get; private set; }
    public Data Data { get; private set; }
    public GameState GameState { get; private set; }

    public Game(MonoEventRelay monoEventRelay, UserInterface userInterface)
    {
        // These are MonoBehaviours so they have to be injected.
        MonoEventRelay = monoEventRelay;
        UserInterface = userInterface;

        InputManager.RegisterMap(new InputMapPC());
        Data = new Data();
        GameState = new GameState(this);
    }
}

public interface IGameDependent
{
    void Initialize(Game game);
}

public class GameBase : MonoBehaviourEx, IGameDependent
{
    public Game Game { get; private set; }

    public virtual void Initialize(Game game)
    {
        Game = game;
    }
}
