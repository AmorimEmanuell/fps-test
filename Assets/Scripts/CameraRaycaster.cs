using System.Collections.Generic;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    [SerializeField] private Transform myTransform;

    private const int RaycastBuffer = 5;
    private const float MaxDistance = 10f;
    private const string MainButtonName = "Fire1";

    private readonly RaycastHit[] results = new RaycastHit[RaycastBuffer];
    private readonly Dictionary<Collider, IInteractable> cachedInteractables = new Dictionary<Collider, IInteractable>();

    private IInteractable currentInteractable;

    private void Update()
    {
        var ray = new Ray(myTransform.position, myTransform.forward);
        var hits = Physics.RaycastNonAlloc(ray, results, MaxDistance);

        if (hits == 0)
        {
            DeselectCurrentInteractable();
            return;
        }

        var hit = GetClosestHit(results, hits);
        if (!cachedInteractables.TryGetValue(hit.collider, out IInteractable interactable))
        {
            var newInteractable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (newInteractable != null)
            {
                cachedInteractables.Add(hit.collider, newInteractable);
                interactable = newInteractable;
            }
        }

        if (currentInteractable != interactable)
        {
            DeselectCurrentInteractable();
            SelectInteractable(interactable);
        }

        CheckForInput();
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

    private void CheckForInput()
    {
        if (Input.GetButtonDown(MainButtonName))
        {
            if (currentInteractable != null)
            {
                currentInteractable.OnPointerClick();
            }
        }
    }

    private RaycastHit GetClosestHit(RaycastHit[] results, int hits)
    {
        var closestDistance = Mathf.Infinity;
        var closestIndex = 0;

        for (var i = 0; i < hits; i++)
        {
            if (results[i].distance < closestDistance)
            {
                closestDistance = results[i].distance;
                closestIndex = i;
            }
        }

        return results[closestIndex];
    }
}
