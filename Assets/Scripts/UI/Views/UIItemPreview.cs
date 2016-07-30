using UnityEngine;
using UnityEngine.UI;
using Models;

namespace UI
{
    public class UIItemPreview : MonoBehaviour
    {
        public ItemManipulator ItemManipulator;
        public Text Name;

        public void SetData(ItemData item)
        {
            ItemManipulator.Reset();

            Name.text = item.Name;
            // TODO: Load up item from item data here.
        }
    }
}
