using UnityEngine;
using Framework;
using System;

public class InteractableObjectListener : MonoBehaviour
{
    public InteractableObjectEvent LookEntered;
    public InteractableObjectEvent LookLeft;

    private RaycastHit lastHit;
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
            if (hit.collider != lastHit.collider)
            {
                if (currentItem != null)
                    LeaveItem();

                hitItem = hit.collider.GetComponent<InteractableObject>();
                if (hitItem != null)
                {
                    currentItem = hitItem;
                    currentItem.LookEnter();

                    if (LookEntered != null)
                        LookEntered.Invoke(currentItem);
                }
            }

            lastHit = hit;
        }
        else if (currentItem != null)
        {
            LeaveItem();
        } 
    }

    public void OnDisable()
    {
        currentItem = null;
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
