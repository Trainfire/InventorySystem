using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InputSystem;
using System;
using UI;
using UnityEngine.Events;

public class ListNavigation : MonoBehaviour, IInputHandler
{
    public event UnityAction<UIDataViewList> Focused;

    private InputHoldBehaviour holdBehaviourDown;
    private InputHoldBehaviour holdBehaviourUp;
    private List<UIDataViewList> lists;
    private int index;

    public void Awake()
    {
        lists = new List<UIDataViewList>();

        holdBehaviourDown = new InputHoldBehaviour(InputAction.Down);
        holdBehaviourDown.OnTrigger += HoldBehaviourDown_OnTrigger;

        holdBehaviourUp = new InputHoldBehaviour(InputAction.Up);
        holdBehaviourUp.OnTrigger += HoldBehaviourUp_OnTrigger;

        InputManager.RegisterHandler(this);
    }

    public void Register(UIDataViewList list)
    {
        if (lists.Contains(list))
        {
            Debug.LogErrorFormat("List '{0}' is already registered.", list.name);
        }
        else
        {
            lists.Add(list);
        }
    }

    public void Unregister(UIDataViewList list)
    {
        if (!lists.Contains(list))
        {
            Debug.LogErrorFormat("List '{0}' has not been registered.", list.name);
        }
        else
        {
            lists.Remove(list);
        }
    }

    public void Focus(UIDataViewList list)
    {
        if (!lists.Contains(list))
        {
            Debug.LogWarningFormat("Cannot focus on list '{0}' as it has not been registered. Call Register first.", list.name);
        }
        else
        {
            index = lists.IndexOf(list);
            Focus(index);
        }
    }

    public void Focus(int index)
    {
        if (index < 0 || index > lists.Count - 1)
        {
            Debug.LogError("Index is out of range.");
        }
        else
        {
            this.index = index;
            if (Focused != null)
                Focused(lists[index]);
        }
    }

    void IInputHandler.HandleInput(InputActionEvent action)
    {
        if (lists.Count == 0)
            return;

        if (action.Type == InputActionType.Down)
        {
            switch (action.Action)
            {
                case InputAction.Up:
                    lists[index].Prev();
                    break;
                case InputAction.Down:
                    lists[index].Next();
                    break;
                case InputAction.Right:
                    FocusNext();
                    break;
                case InputAction.Left:
                    FocusPrev();
                    break;
                default:
                    break;
            }
        }
    }

    private void HoldBehaviourUp_OnTrigger()
    {
        lists[index].Prev();
    }

    private void HoldBehaviourDown_OnTrigger()
    {
        lists[index].Next();
    }

    private void FocusPrev()
    {
        if ((index - 1) >= 0)
        {
            index--;
            Focus(index);
        }
    }

    private void FocusNext()
    {
        if ((index + 1) < lists.Count)
        {
            index++;
            Focus(index);
        }
    }

    public void OnDestroy()
    {
        holdBehaviourDown.OnTrigger -= HoldBehaviourDown_OnTrigger;
        holdBehaviourUp.OnTrigger -= HoldBehaviourUp_OnTrigger;

        holdBehaviourDown.Destroy();
        holdBehaviourUp.Destroy();
    }
}
