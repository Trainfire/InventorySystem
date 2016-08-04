using UnityEngine;
using UnityEngine.UI;
using Framework;
using Framework.UI;
using Models;

public class UIWorldItem : UIDataView<ItemData>
{
    [SerializeField] private Text Name;
    [SerializeField] private Text Prompt;

    private const float FadeDuration = 0.1f;

    public void Awake()
    {
        _disableOnHide = false;
    }

    protected override void OnShow()
    {
        gameObject.GetOrAddComponent<Fader>().Fade(FadeType.In, FadeDuration);
    }

    protected override void OnHide()
    {
        gameObject.GetOrAddComponent<Fader>().Fade(FadeType.Out, FadeDuration);
    }

    public override void OnSetData(ItemData data)
    {
        Name.text = data.Name;
    }

    public void SetPrompt(string label)
    {
        Prompt.text = label;
    }
}
