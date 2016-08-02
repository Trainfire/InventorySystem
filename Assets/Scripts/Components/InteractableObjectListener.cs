using UnityEngine;

public class InteractableObjectListener : MonoBehaviour
{
    public InteractableObjectEvent LookEntered;
    public InteractableObjectEvent LookLeft;

    private InteractableObject currentItem;

    public void Awake()
    {
        LookEntered = new InteractableObjectEvent();
        LookLeft = new InteractableObjectEvent();
    }

    public void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        InteractableObject hitItem;

        if (Physics.Raycast(ray, out hit))
        {
            hitItem = hit.collider.GetComponent<InteractableObject>();
            if (hitItem != null)
            {
                if (currentItem != hitItem)
                {
                    LeaveItem();
                    currentItem = hitItem;
                    currentItem.LookEnter();

                    if (LookEntered != null)
                        LookEntered.Invoke(currentItem);
                }
            }
        }
        else
        {
            LeaveItem();
        }
    }

    private void LeaveItem()
    {
        if (currentItem != null)
        {
            currentItem.LookLeave();

            if (LookLeft != null)
                LookLeft.Invoke(currentItem);

            currentItem = null;
        }
    }
}
