using UnityEngine;
using Framework;
using System.Collections;
using System.Collections.Generic;

public class Bootstrapper : MonoBehaviour
{
    public MonoEventRelay MonoEventRelay;
    public UserInterface UserInterface;
    public GameState GameState;

    public void Awake()
    {
        // Create the Game wrapper.
        var game = new Game(MonoEventRelay, UserInterface);

        // Inject dependency here.
        UserInterface.Initialize(game);
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
        GameState = new GameState();
    }
}

public interface IGameDependent
{
    void Initialize(Game game);
}
