using UnityEngine;
using UnityEngine.UI;
using Framework;

class UITweenTest : MonoBehaviour
{
    private Image image;
    private TweenColor tween;

    public void Awake()
    {
        image = GetComponent<Image>();

        image.CrossFadeColor(Color.black, 1f, false, false);
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            image.CrossFadeColor(Color.white, 1f, false, false);
        }
    }
}
