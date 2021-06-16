using System.Collections.Generic;
using UnityEngine;

public class InteractionRaycaster : MonoBehaviour
{
    [SerializeField] private Transform myTransform;

    private const int RaycastBuffer = 5;
    private const float MaxDistance = 10f;
    private const float DistanceInFront = 2f;
    private const string MainButtonName = "Fire1";

    private readonly RaycastHit[] results = new RaycastHit[RaycastBuffer];
    private readonly Dictionary<Collider, IInteractable> cachedInteractables = new Dictionary<Collider, IInteractable>();

    private IInteractable currentInteractable;
    private Mode currentMode = Mode.Interaction;
    private Collectable currentCollectable;

    public enum Mode
    {
        Place,
        Interaction
    }

    private void Awake()
    {
        EventManager.Register(EventManager.EventType.PlaceModeActivate, PlaceModeActivateHandler);
    }

    private void Update()
    {
        var ray = new Ray(myTransform.position, myTransform.forward);
        var hits = Physics.RaycastNonAlloc(ray, results, MaxDistance);

        if (currentMode == Mode.Interaction)
        {
            HandleInteractionMode(results, hits);
        }
        else
        {
            HandlePlaceMode(results, hits);
        }
    }

    private void HandleInteractionMode(RaycastHit[] results, int hits)
    {
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

    public void PlaceModeActivateHandler(object obj)
    {
        if (obj is Collectable collectable)
        {
            currentCollectable = collectable;
            currentMode = Mode.Place;
        }
    }

    private void HandlePlaceMode(RaycastHit[] results, int hits)
    {
        if (Input.GetButtonUp(MainButtonName))
        {
            currentCollectable.Collider.enabled = true;
            currentCollectable = null;
            currentMode = Mode.Interaction;
            return;
        }

        if (hits == 0)
        {
            PlaceCollectableInFront();
            return;
        }

        var hit = GetClosestHit(results, hits);
        currentCollectable.transform.position = hit.point;
    }

    private void PlaceCollectableInFront()
    {
        currentCollectable.transform.position = myTransform.position + (myTransform.forward.normalized * DistanceInFront);
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
