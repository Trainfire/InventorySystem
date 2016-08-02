using UnityEngine;
using System;
using Framework;

public class MenuAnimation : MonoBehaviourEx
{
    public event Action TransitionOutFinished;

    private CanvasGroup _canvasGroup;
    private TweenFloat _tweenAlpha;
    private TweenVector _tweenScale;

    public void Awake()
    {
        _canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;

        _tweenAlpha = new TweenFloat();
        _tweenAlpha.Duration = 0.2f;

        _tweenScale = new TweenVector();
        _tweenScale.Duration = 0.1f;
    }

    public void TransitionIn()
    {
        _canvasGroup.transform.localScale = Vector3.one * 1.5f;

        _tweenAlpha.From = _canvasGroup.alpha;
        _tweenAlpha.To = 1f;
        _tweenAlpha.Play();

        _tweenScale.From = _canvasGroup.transform.localScale;
        _tweenScale.To = Vector3.one;
        _tweenScale.Play();
    }

    public void TransitionOut()
    {
        _tweenAlpha.From = _canvasGroup.alpha;
        _tweenAlpha.To = 0f;
        _tweenAlpha.Play(OnTransitionOutComplete);

        _tweenScale.From = _canvasGroup.transform.localScale;
        _tweenScale.To = Vector3.one * 1f;
        _tweenScale.Play();
    }

    private void OnTransitionOutComplete()
    {
        if (TransitionOutFinished != null)
            TransitionOutFinished.Invoke();
    }

    public void Update()
    {
        _canvasGroup.alpha = _tweenAlpha.Value;
        _canvasGroup.transform.localScale = _tweenScale.Value;
    }
}
