using UnityEngine;
using Framework;
using System.Collections;
using System.Collections.Generic;

public class Bootstrapper : MonoBehaviour
{
    public MonoEventRelay MonoEventRelay;
    public UserInterface UserInterface;

    public void Awake()
    {
        // Initialize objects if not a MonoBehaviour.
        InputManager.RegisterMap(new InputMapPC());
        var data = new Data();

        // Create the Game wrapper.
        var game = new Game(MonoEventRelay, UserInterface, data);

        // Inject dependency here.
        UserInterface.Initialize(game);
    }
}

public class Game
{
    public MonoEventRelay MonoEventRelay { get; private set; }
    public UserInterface UserInterface { get; private set; }
    public Data Data { get; private set; }

    public Game(MonoEventRelay monoEventRelay, UserInterface userInterface, Data data)
    {
        MonoEventRelay = monoEventRelay;
        UserInterface = userInterface;
        Data = data;
    }
}

public interface IGameDependent
{
    void Initialize(Game game);
}
