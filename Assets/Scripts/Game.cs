using UnityEngine;
using Framework;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    public MonoEventRelay MonoEventRelay { get; private set; }
    public UserInterface UserInterface { get; private set; }
    public Data Data { get; private set; }

    public void Start()
    {
        gameObject.AddComponent<MonoEventRelay>();

        InputManager.RegisterMap(new InputMapPC());

        Data = new Data();

        UserInterface = new UserInterface(this);
    }
}
