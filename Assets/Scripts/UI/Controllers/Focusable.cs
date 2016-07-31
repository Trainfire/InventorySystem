using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/// <summary>
/// Marks a GameObject as focusable and triggers appropriate callbacks.
/// </summary>
public class Focusable : MonoBehaviour
{
    public UnityEvent OnAwake;
    public UnityEvent Focused;
    public UnityEvent Unfocused;

    public void Awake()
    {
        OnAwake.Invoke();
    }

    public void Focus()
    {
        Focused.Invoke();
    }

    public void Unfocus()
    {
        Unfocused.Invoke();
    }
}
