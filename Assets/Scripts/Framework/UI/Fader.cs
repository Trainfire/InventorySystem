using UnityEngine;

namespace Framework.UI
{
    public enum FadeType
    {
        In,
        Out,
    }

    public class Fader : MonoBehaviourEx
    {
        private TweenFloat _tweenAlpha;
        private CanvasGroup _canvasGroup;

        public void Awake()
        {
            _canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;

            _tweenAlpha = new TweenFloat();
        }

        public void Fade(FadeType fadeType, float duration)
        {
            _tweenAlpha.From = _canvasGroup.alpha;
            _tweenAlpha.To = fadeType == FadeType.In ? 1f : 0f;
            _tweenAlpha.Duration = duration;
            _tweenAlpha.Play();
        }

        public void Update()
        {
            _canvasGroup.alpha = _tweenAlpha.Value;
        }
    }
}
