using UnityEngine;
using System.Collections;
using Framework;
using System;

public class PlayerInput : GameBase, IInputHandler, IStateListener
{
    public InteractableObjectListener InteractableObjectListener;

    private InteractableObject currentInteractable;

    public void Awake()
    {
        InputManager.RegisterHandler(this);
        Game.GameState.StateManager.RegisterListener(this);

        InteractableObjectListener.LookEntered.AddListener((obj) => currentInteractable = obj);
    }

    void IInputHandler.HandleInput(InputActionEvent action)
    {
        if (Game.GameState.State == State.Paused)
            return;

        if (action.Action == InputAction.Interact)
            OnInteract(action.Type);
    }

    void OnInteract(InputActionType actionType)
    {
        if (actionType == InputActionType.Down)
        {
            if (currentInteractable != null)
            {
                var item = currentInteractable.GetComponent<Item>();
                if (item != null)
                {
                    Game.Data.Inventory.AddItem(item.ItemData);
                    item.Pickup();
                }
            }
        }
    }

    void IStateListener.OnStateChanged(State state)
    {
        InteractableObjectListener.enabled = state == State.Running;
    }
}
