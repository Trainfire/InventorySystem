using UnityEngine;
using UI;

class AnimatorDataViewScroller : MonoBehaviour
{
    private AnimatorScroller scroller;
    private UIDataViewList dataView;

    public void Awake()
    {
        scroller = gameObject.AddComponent<AnimatorScroller>();
        dataView = GetComponent<UIDataViewList>();
        dataView.Highlighted += DataView_Highlighted;
    }

    private void DataView_Highlighted(UIDataViewSelectable view)
    {
        scroller.ScrollTo(view.transform);
    }
}
