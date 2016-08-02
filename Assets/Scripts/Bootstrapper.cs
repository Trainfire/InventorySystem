using UnityEngine;
using Framework;
using System.Collections;
using System.Collections.Generic;

public class Bootstrapper : MonoBehaviour
{
    public UserInterface UserInterface;
    public PlayerInput PlayerInput;

    public MonoEventRelay MonoEventRelay;

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

public class MonoBehaviourEx : MonoBehaviour, IGameDependent
{
    public Game Game { get; private set; }

    public virtual void Initialize(Game game)
    {
        Game = game;
    }
}
