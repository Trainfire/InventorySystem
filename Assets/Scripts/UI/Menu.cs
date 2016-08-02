using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Framework;

public class Menu : MonoBehaviourEx
{
    [SerializeField] private UIMenuInventory _inventory;

    // Animation stuff
    private MenuAnimation _animation;

    protected override void OnFirstShow()
    {
        _disableOnHide = false;
        _animation = gameObject.AddComponent<MenuAnimation>();
        _animation.TransitionOutFinished += Animation_TransitionOutFinished;
    }

    private void Animation_TransitionOutFinished()
    {
        _inventory.SetVisibility(false);
        _inventory.gameObject.SetActive(false);
    }

    protected override void OnShow()
    {
        _inventory.gameObject.GetComponent<InputGroupHandler>((comp) => comp.InputEnabled = true);
        _inventory.SetVisibility(true);
        _animation.TransitionIn();
    }

    protected override void OnHide()
    {
        _animation.TransitionOut();
        _inventory.gameObject.GetComponent<InputGroupHandler>((comp) => comp.InputEnabled = false);
    }
}
