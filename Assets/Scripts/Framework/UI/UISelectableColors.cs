using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Framework.Stylesheet;

namespace Framework.UI
{
    public class UISelectableColors : MonoBehaviour
    {
        public List<Graphic> Affectors;
        public ColorData SelectedColor;
        public ColorData HighlightedColor;
        public ColorData DefaultColor;

        private UIDataViewSelectable selectable;

        public void Awake()
        {
            if (Affectors == null)
                Affectors = new List<Graphic>();

            selectable = GetComponent<UIDataViewSelectable>();
            if (selectable != null)
            {
                selectable.Defaulted += (arg) => Affectors.ForEach(x => x.color = DefaultColor.Color);
                selectable.Highlighted += (arg) => Affectors.ForEach(x => x.color = HighlightedColor.Color);
                selectable.Selected += (arg) => Affectors.ForEach(x => x.color = SelectedColor.Color);
            }
        }
    }
}
