using System.Collections.Generic;
using UnityEngine;

public class SelectionInteractionMode : InteractionMode
{
    private readonly Dictionary<Collider, IInteractable> cachedInteractables = new Dictionary<Collider, IInteractable>();
    private IInteractable currentInteractable;

    public SelectionInteractionMode(LayerMask layerMask) : base(layerMask)
    {
    }

    public override void HandleInteraction(RaycastHit[] results, int hits, RaycastHit closestHit)
    {
        if (!cachedInteractables.TryGetValue(closestHit.collider, out IInteractable interactable))
        {
            var newInteractable = closestHit.collider.gameObject.GetComponent<IInteractable>();
            if (newInteractable != null)
            {
                cachedInteractables.Add(closestHit.collider, newInteractable);
                interactable = newInteractable;
            }
        }

        if (currentInteractable != interactable)
        {
            DeselectCurrentInteractable();
            SelectInteractable(interactable);
        }
    }

    public override void HandleZeroHits()
    {
        DeselectCurrentInteractable();
    }

    private void DeselectCurrentInteractable()
    {
        if (currentInteractable != null)
        {
            currentInteractable.OnPointerExit();
            currentInteractable = null;
        }
    }

    private void SelectInteractable(IInteractable interactable)
    {
        currentInteractable = interactable;
        if (currentInteractable != null)
        {
            currentInteractable.OnPointerEnter();
        }
    }

    public override void HandleInteractionUp()
    {
    }

    public override void HandleInteractionDown()
    {
        if (currentInteractable != null)
        {
            currentInteractable.OnPointerClick();
        }
    }
}
