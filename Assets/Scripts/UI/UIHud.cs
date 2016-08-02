using UnityEngine;
using Framework;
using Framework.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIHud : GameBase, IStateListener
{
    public UIWorldItem worldItemPrototype;
    public InteractableObjectListener InteractableListener;

    private UIWorldItem worldItemInstance;

    public override void Initialize(Game game)
    {
        base.Initialize(game);
        game.GameState.StateManager.RegisterListener(this);
    }

    public void Start()
    {
        InteractableListener.LookEntered.AddListener(obj => OnObjectLookedAt(obj));
        InteractableListener.LookLeft.AddListener(obj => RemoveItemUI());
    }

    void OnObjectLookedAt(InteractableObject obj)
    {
        var item = obj.GetComponent<Item>();
        if (item)
            ShowItem(item);
    }

    void ShowItem(Item item)
    {
        worldItemInstance = UIUtility.Add<UIWorldItem>(transform, worldItemPrototype.gameObject);
        worldItemInstance.SetData(item.ItemData);
        worldItemInstance.SetPrompt("Take (E)");

        // Listen for item pickup so we can cleanup UI.
        item.PickedUp.AddListener(OnItemPickedUp);
    }

    void OnItemPickedUp(Item item)
    {
        RemoveItemUI();
    }

    void RemoveItemUI()
    {
        if (worldItemInstance != null)
            Destroy(worldItemInstance.gameObject);
    }

    void IStateListener.OnStateChanged(State state)
    {
        if (state == State.Paused)
        {
            RemoveItemUI();
        }
    }
}
