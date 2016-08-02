using UnityEngine;
using UnityEngine.UI;
using Framework.UI;
using Models;

public class UIWorldItem : UIDataView<ItemData>
{
    public Text Name;
    public Text Prompt;

    public override void OnSetData(ItemData data)
    {
        Name.text = data.Name;
    }

    public void SetPrompt(string label)
    {
        Prompt.text = label;
    }
}
