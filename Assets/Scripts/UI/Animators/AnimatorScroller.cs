using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimatorScroller : MonoBehaviour
{
    private RectTransform rectTransform;

    private float duration = 0.1f;
    private AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    private float time;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private bool doScroll;

    public void Awake()
    {
        rectTransform = transform as RectTransform;
    }

    public void ScrollTo(Transform target)
    {
        var targetRect = target as RectTransform;
        var targetY = Mathf.Abs(targetRect.anchoredPosition.y) + targetRect.rect.height / 2f;

        startPosition = rectTransform.anchoredPosition;
        targetPosition = new Vector2(rectTransform.anchoredPosition.x, targetY);

        time = 0f;
        doScroll = true;
    }

    public void Update()
    {
        if (doScroll)
        {
            if (time < duration)
            {
                rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, curve.Evaluate(time / duration));
                time += Time.deltaTime;
            }
            else
            {
                doScroll = false;
            }
        }
    }

    IEnumerator Scroll(Vector2 target)
    {
        var position = rectTransform.anchoredPosition;

        while (time < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(position, target, curve.Evaluate(time / duration));
            time += Time.deltaTime;
            yield return 0;
        }

        yield return null;
    }
}
