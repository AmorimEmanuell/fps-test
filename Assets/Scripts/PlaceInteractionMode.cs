using UnityEngine;

public class PlaceInteractionMode : InteractionMode
{
    private const float DistanceInFront = 2f;

    private Collectable currentCollectable;
    private Transform myTransform;
    private InteractionRaycaster interactionRaycaster;

    public PlaceInteractionMode(InteractionRaycaster interactionRaycaster, Transform myTransform)
    {
        this.myTransform = myTransform;
        this.interactionRaycaster = interactionRaycaster;

        EventManager.Register(EventManager.EventType.PlaceModeActivate, PlaceModeActivateHandler);
    }

    public override void HandleInteraction(RaycastHit[] results, int hits, RaycastHit closestHit)
    {
        currentCollectable.transform.position = closestHit.point;
    }

    public override void HandleZeroHits()
    {
        PlaceCollectableInFront();
    }

    private void PlaceCollectableInFront()
    {
        currentCollectable.transform.position = myTransform.position + (myTransform.forward.normalized * DistanceInFront);
    }

    private void PlaceModeActivateHandler(object obj)
    {
        if (obj is Collectable collectable)
        {
            currentCollectable = collectable;
            interactionRaycaster.SwitchMode(InteractionRaycaster.Mode.Place);
        }
    }

    public override void HandleInteractionUp()
    {
        currentCollectable.Collider.enabled = true;
        currentCollectable = null;

        interactionRaycaster.SwitchMode(InteractionRaycaster.Mode.Selection);
    }

    public override void HandleInteractionDown()
    {
    }
}
