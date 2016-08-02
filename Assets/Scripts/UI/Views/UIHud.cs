using UnityEngine;
using Framework.UI;
using System.Collections;

public class UIHud : MonoBehaviour
{
    public UIWorldItem worldItemPrototype;

    private InteractableObjectListener interactableListener;
    private UIWorldItem worldItemInstance;

    public void Start()
    {
        interactableListener = gameObject.AddComponent<InteractableObjectListener>();

        interactableListener.LookEntered.AddListener((obj) =>
        {
            var item = obj.GetComponent<Item>();
            if (item)
                ShowItem(item);
        });

        interactableListener.LookLeft.AddListener((obj) =>
        {
            Destroy(worldItemInstance.gameObject);
        });
    }

    void ShowItem(Item item)
    {
        worldItemInstance = UIUtility.Add<UIWorldItem>(transform, worldItemPrototype.gameObject);
        worldItemInstance.SetData(item.ItemData);
        worldItemInstance.SetPrompt("Take (E)");
    }
}
