using UnityEngine;
using UnityEngine.UI;
using System;
using Framework;

public class MenuTransitionAnimation : MonoBehaviourEx
{
    private MenuBase _out;
    private MenuBase _in;
    private Action<MenuBase> _onComplete;
    private TweenFloat _tweenAlphaIn;
    private TweenFloat _tweenAlphaOut;
    private CanvasGroup _outCanvas;
    private CanvasGroup _inCanvas;
    private bool _transitioning;

    private const float TransitionTime = 0.1f;

    public void Awake()
    {
        _tweenAlphaIn = new TweenFloat();
        _tweenAlphaIn.From = 0f;
        _tweenAlphaIn.To = 1f;

        _tweenAlphaOut = new TweenFloat();
        _tweenAlphaOut.From = 1f;
        _tweenAlphaOut.To = 0f;
    }

    public void Transition(MenuBase from, MenuBase to, Action<MenuBase> onComplete)
    {
        _out = from;
        _in = to;

        _in.SetVisibility(true);

        _outCanvas = from.gameObject.GetOrAddComponent<CanvasGroup>();
        _inCanvas = to.gameObject.GetOrAddComponent<CanvasGroup>();

        _onComplete = onComplete;

        _tweenAlphaIn.Duration = TransitionTime;
        _tweenAlphaOut.Duration = TransitionTime;
        _tweenAlphaIn.Play();
        _tweenAlphaOut.Play();

        _transitioning = true;
    }

    public void Update()
    {
        if (_transitioning)
        {
            _outCanvas.alpha = _tweenAlphaOut.Value;
            _inCanvas.alpha = _tweenAlphaIn.Value;

            if (!_tweenAlphaIn.Tweening && !_tweenAlphaOut.Tweening)
            {
                _out.SetVisibility(false);
                _transitioning = false;
                _onComplete(_in);
                _onComplete = null;
            }
        }
    }
}
