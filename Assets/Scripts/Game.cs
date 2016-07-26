using UnityEngine;
using InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    public InputManager InputManager { get; private set; }
    public MonoEventRelay MonoEventRelay { get; private set; }

    public void Start()
    {
        gameObject.AddComponent<MonoEventRelay>();

        InputManager = new InputManager();
        InputManager.RegisterMap(new InputMapPC());

        new InputTest(this);
    }
}

public interface IMonoUpdateReceiver
{
    void OnUpdate();
}

public interface IMonoLateUpdateReceiver
{
    void OnLateUpdate();
}