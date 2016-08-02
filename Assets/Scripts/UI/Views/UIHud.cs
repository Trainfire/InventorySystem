using UnityEngine;
using Framework.UI;
using System.Collections;

public class UIHud : MonoBehaviour
{
    public UIWorldItem worldItemPrototype;
    public InteractableObjectListener InteractableListener;

    private UIWorldItem worldItemInstance;

    public void Start()
    {
        InteractableListener.LookEntered.AddListener(obj => OnObjectLookedAt(obj));
        InteractableListener.LookLeft.AddListener(obj => RemoveItemUI());
    }

    void OnObjectLookedAt(InteractableObject obj)
    {
        var item = obj.GetComponent<Item>();
        if (item)
            ShowItem(item);
    }

    void ShowItem(Item item)
    {
        worldItemInstance = UIUtility.Add<UIWorldItem>(transform, worldItemPrototype.gameObject);
        worldItemInstance.SetData(item.ItemData);
        worldItemInstance.SetPrompt("Take (E)");

        // Listen for item pickup so we can cleanup UI.
        item.PickedUp.AddListener(OnItemPickedUp);
    }

    void OnItemPickedUp(Item item)
    {
        RemoveItemUI();
    }

    void RemoveItemUI()
    {
        Destroy(worldItemInstance.gameObject);
    }
}
